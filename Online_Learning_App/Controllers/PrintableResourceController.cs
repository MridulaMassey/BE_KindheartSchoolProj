using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Infrastructure;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_App_Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrintableResourceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PrintableResourceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePrintableResource([FromBody] PrintableResourceCreateRequest dto)
        {
            if (dto == null)
                return BadRequest("Invalid request payload.");

            if (string.IsNullOrWhiteSpace(dto.FileUrl))
                return BadRequest("File URL is required.");

            var resource = new PrintableResource
            {
                Title = dto.Title,
                Description = dto.Description,
                FileUrl = dto.FileUrl,
                TeacherId = dto.TeacherId,
                UploadDate = DateTime.UtcNow,
             //   ClassGroupId = dto.ClassGroupId,
              //  SubjectId = dto.SubjectId,
                IsActive = true // ← Ensure resources are visible in Get endpoints
            };

            _context.PrintableResources.Add(resource);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Resource successfully created." });
        }

        [HttpGet("classgroup/{classGroupId}")]
        public async Task<ActionResult<IEnumerable<PrintableResourceDto>>> GetByClassGroup(Guid classGroupId)
        {
            var resources = await _context.PrintableResources
                .Where(r => r.ClassGroupId == classGroupId && r.IsActive)
                .Select(r => new PrintableResourceDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    FileUrl = r.FileUrl,
                    Description = r.Description,
                    UploadDate = r.UploadDate,
                    TeacherId = r.TeacherId,
                    ClassGroupId = r.ClassGroupId,
                    SubjectId = r.SubjectId
                })
                .ToListAsync();

            return Ok(resources);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PrintableResourceDto>>> GetAllPrintableResources()
        {
            var resources = await _context.PrintableResources
                .Where(r => r.IsActive)
                .OrderByDescending(r => r.UploadDate)
                .Select(r => new PrintableResourceDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    FileUrl = r.FileUrl,
                    Description = r.Description,
                    TeacherId = r.TeacherId,
                    UploadDate = r.UploadDate
                })
                .ToListAsync();

            return Ok(resources);
        }
    }
}
