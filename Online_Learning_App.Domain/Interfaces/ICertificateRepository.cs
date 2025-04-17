using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App.Domain.Interfaces
{
    public interface ICertificateRepository
    {
        Task<IEnumerable<Certificate>> GetByStudentIdAsync(Guid studentId);
        Task<Certificate?> GetByIdAsync(Guid certificateId);
        Task AddAsync(Certificate certificate);
        Task UpdateAsync(Certificate certificate);
    }
}
