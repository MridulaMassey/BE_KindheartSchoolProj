using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_App.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Online_Learning_APP.Application.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;

        public CertificateService(ICertificateRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        public async Task<IEnumerable<Certificate>> GetCertificatesByStudentIdAsync(Guid studentId)
        {
            return await _certificateRepository.GetByStudentIdAsync(studentId);
        }

        public async Task<Certificate?> GetCertificateByIdAsync(Guid certificateId)
        {
            return await _certificateRepository.GetByIdAsync(certificateId);
        }

        public async Task AddCertificateAsync(Certificate certificate)
        {
            await _certificateRepository.AddAsync(certificate);
        }

        public async Task RevokeCertificateAsync(Guid certificateId)
        {
            var cert = await _certificateRepository.GetByIdAsync(certificateId);
            if (cert != null)
            {
                cert.Revoke();
                await _certificateRepository.UpdateAsync(cert);
            }
        }
    }
}
