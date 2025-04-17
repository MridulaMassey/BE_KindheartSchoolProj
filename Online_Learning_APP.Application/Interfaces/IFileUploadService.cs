using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(byte[] fileBytes, string fileName);
    }
}
