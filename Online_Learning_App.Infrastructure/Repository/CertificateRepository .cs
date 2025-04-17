using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;

namespace Online_Learning_App.Infrastructure.Repository
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly ApplicationDbContext _context;

        public CertificateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Certificate>> GetByStudentIdAsync(Guid studentId)
        {
            return await _context.Certificates
                .Where(c => c.StudentId == studentId && !c.IsRevoked)
                .ToListAsync();
        }

        public async Task<Certificate?> GetByIdAsync(Guid certificateId)
        {
            return await _context.Certificates.FindAsync(certificateId);
        }

        public async Task AddAsync(Certificate certificate)
        {
            await _context.Certificates.AddAsync(certificate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Certificate certificate)
        {
            _context.Certificates.Update(certificate);
            await _context.SaveChangesAsync();
        }
    }
}
