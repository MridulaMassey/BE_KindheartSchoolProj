using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_App_Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService _certificateService;

        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        // GET: api/certificate/student/{studentId}
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetCertificatesByStudent(Guid studentId)
        {
            var certificates = await _certificateService.GetCertificatesByStudentIdAsync(studentId);
            return Ok(certificates);
        }

        // GET: api/certificate/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCertificate(Guid id)
        {
            var certificate = await _certificateService.GetCertificateByIdAsync(id);
            if (certificate == null)
                return NotFound();
            return Ok(certificate);
        }

        // POST: api/certificate
        [HttpPost]

        public async Task<IActionResult> AddCertificate([FromBody] CreateCertificateDto dto)
        {
            var certificate = new Certificate
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                CertificateUrl = dto.CertificateUrl,
                StudentId = dto.StudentId,
                TeacherId = dto.TeacherId,
                DateIssued = DateTime.UtcNow,
                IsRevoked = false

            };
            await _certificateService.AddCertificateAsync(certificate);
            return Ok(new { message = "Certificate added successfully." });
        }

        // PUT: api/certificate/revoke/{id}
        [HttpPut("revoke/{id}")]
        public async Task<IActionResult> RevokeCertificate(Guid id)
        {
            await _certificateService.RevokeCertificateAsync(id);
            return Ok(new { message = "Certificate revoked successfully." });
        }
    }
}
