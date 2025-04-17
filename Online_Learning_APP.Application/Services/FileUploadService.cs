using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_APP.Application.Services
{
    public class FileUploadService: IFileUploadService
    {
        private readonly IGoogleDriveService _googleDriveService;

        public FileUploadService(IGoogleDriveService googleDriveService)
        {
            _googleDriveService = googleDriveService;
        }

        public async Task<string> UploadFileAsync(byte[] fileBytes, string fileName)
        {
            if (fileBytes == null || fileBytes.Length == 0)
                throw new ArgumentException("File cannot be empty.");

            return await _googleDriveService.UploadFileAsync(fileBytes, fileName);
        }
    }
}
