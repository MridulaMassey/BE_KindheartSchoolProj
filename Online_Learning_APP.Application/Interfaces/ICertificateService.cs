using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface ICertificateService
    {
        Task<IEnumerable<Certificate>> GetCertificatesByStudentIdAsync(Guid studentId);
        Task<Certificate?> GetCertificateByIdAsync(Guid certificateId);
        Task AddCertificateAsync(Certificate certificate);
        Task RevokeCertificateAsync(Guid certificateId);
    }

}
