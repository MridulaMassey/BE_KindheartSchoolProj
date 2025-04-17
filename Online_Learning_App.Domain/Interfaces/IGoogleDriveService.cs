using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Interfaces
{
    public interface IGoogleDriveService
    {
        Task<string> UploadFileAsync(byte[] fileBytes, string fileName);
    }
}
