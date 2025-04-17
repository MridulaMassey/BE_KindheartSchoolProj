
using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.Services;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoogleDriveUploader.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class FileUploadController : ControllerBase
    {
        private readonly FileUploadService _fileUploadService;

        public FileUploadController(FileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost]
        //public async Task<IActionResult> UploadFile([FromForm] FileUploadRequest request)
        //{
        //    try
        //    {
        //        using var memoryStream = new MemoryStream();
        //        await request.File.CopyToAsync(memoryStream);
        //        byte[] fileBytes = memoryStream.ToArray();

        //        string downloadUrl = await _fileUploadService.UploadFileAsync(fileBytes, request.File.FileName);
        //        return Ok(new { downloadUrl });

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}
        public async Task<IActionResult> UploadFile([FromForm] FileUploadRequest request)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await request.File.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                string downloadUrl = await _fileUploadService.UploadFileAsync(fileBytes, request.File.FileName);

                // 🔍 Extract Google Drive file ID from download URL
                var fileIdMatch = Regex.Match(downloadUrl, @"id=([^&]+)");
                var fileId = fileIdMatch.Success ? fileIdMatch.Groups[1].Value : null;

                // 🛑 Return only downloadUrl if ID isn't extractable
                if (fileId == null)
                    return Ok(new { downloadUrl });

                // ✅ Construct the preview URL
                string previewUrl = $"https://drive.google.com/file/d/{fileId}/preview";

                // ✅ Return both
                return Ok(new
                {
                    downloadUrl,
                    previewUrl
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }

    public class FileUploadRequest
    {
        public IFormFile File { get; set; }
    }
}
