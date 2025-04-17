using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.Extensions.Configuration;
using Online_Learning_App.Domain.Interfaces;

namespace Online_Learning_App.Infrastructure.Service
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private readonly DriveService _driveService;

        public GoogleDriveService(IConfiguration configuration)
        {
            // Read the relative path from appsettings.json
            string relativePath = configuration["GoogleDrive:ServiceAccountJson"];

            // Convert relative path to absolute path
            string serviceAccountJsonPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            if (!File.Exists(serviceAccountJsonPath))
            {
                throw new FileNotFoundException($"Google Drive service account JSON file not found at: {serviceAccountJsonPath}");
            }

            GoogleCredential credential;
            using (var stream = new FileStream(serviceAccountJsonPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(DriveService.ScopeConstants.DriveFile);
            }

            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveUploader"
            });
        }

        public async Task<string> UploadFileAsync(byte[] fileBytes, string fileName)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName
            };

            using (var stream = new MemoryStream(fileBytes))
            {
                var request = _driveService.Files.Create(fileMetadata, stream, "application/pdf");
                request.Fields = "id";
                var file = await request.UploadAsync();

                if (file.Status != UploadStatus.Completed)
                {
                    throw new Exception("File upload failed.");
                }

                var fileId = request.ResponseBody.Id;

                // Set file permission: Anyone can download but NOT view in Drive
                var permission = new Google.Apis.Drive.v3.Data.Permission
                {
                    Type = "anyone",
                    Role = "writer",
                    AllowFileDiscovery = true,
                  
                };

                await _driveService.Permissions.Create(permission, fileId).ExecuteAsync();

                return $"https://drive.google.com/uc?export=download&id={fileId}";
            }
        }
    }

}
