using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Infrastructure;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_App_Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KindnessJournalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KindnessJournalController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEntry([FromBody] KindnessJournalDto dto)
        {
            var journal = new KindnessJournal
            {
                StudentId = dto.StudentId,
                EntryText = dto.EntryText,
                Emoji = dto.Emoji,
                EntryDate = DateTime.Now
            };

            _context.KindnessJournals.Add(journal);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Journal entry saved!" });
        }



        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudent(Guid studentId)
        {
            var entries = await _context.KindnessJournals
                .Where(j => j.StudentId == studentId)
                .OrderByDescending(j => j.EntryDate)
                .ToListAsync();

            return Ok(entries);
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllJournals()
        {
            var journals = await _context.KindnessJournals
                .Include(j => j.Student)
                .OrderByDescending(j => j.EntryDate)
                .Select(j => new
                {
                    studentName = j.Student.UserName,
                    entryText = j.EntryText,
                    entryDate = j.EntryDate,
                    journalId = j.JournalId
                })
                .ToListAsync();

            return Ok(journals);
        }

    }
}
