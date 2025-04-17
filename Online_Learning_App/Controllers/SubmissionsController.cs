using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Infrastructure;
using Online_Learning_APP.Application.DTO;


namespace Online_Learning_App_Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionsController : ControllerBase
    {
       
        private readonly ApplicationDbContext _context;

        public SubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitActivity([FromBody] SubmissionDto dto)
        {
            var studentExists = await _context.Students.AnyAsync(s => s.Id == dto.StudentId);
            var activityExists = await _context.Activities.AnyAsync(a => a.Id == dto.ActivityId);

            if (!studentExists || !activityExists)
                return BadRequest("Invalid student or activity.");

            // 🔍 Check if this student already submitted for this activity
            var existingSubmission = await _context.Submissions
                .FirstOrDefaultAsync(s => s.StudentId == dto.StudentId && s.ActivityId == dto.ActivityId);

            if (existingSubmission != null)
            {
                //If submission exists, update it instead of creating a new one
                existingSubmission.PdfUrl = dto.PdfUrl ?? "";
                existingSubmission.SubmissionDate = DateTime.UtcNow;
                existingSubmission.StudentComment = dto.StudentComment ?? "";

                _context.Submissions.Update(existingSubmission);
                await _context.SaveChangesAsync();

                return Ok("You have already submitted this activity. Your submission has been updated.");
            }

            // New submission
            var submission = new Submission
            {
                SubmissionId = Guid.NewGuid(),
                ActivityId = dto.ActivityId,
                StudentId = dto.StudentId,
                PdfUrl = dto.PdfUrl,
                SubmissionDate = DateTime.UtcNow,
                Feedback = "",
                Grade = 0,
                StudentComment = dto.StudentComment
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return Ok("Activity submitted successfully.");
        }

        [HttpPut("resubmit")]
        public async Task<IActionResult> ResubmitActivity([FromBody] SubmissionDto dto)
        {
            var submission = await _context.Submissions
                .FirstOrDefaultAsync(s => s.StudentId == dto.StudentId && s.ActivityId == dto.ActivityId);

            if (submission == null)
                return NotFound("Submission not found.");

            var activity = await _context.Activities.FirstOrDefaultAsync(a => a.Id == dto.ActivityId);

            if (activity == null)
                return NotFound("Activity not found.");

            if (DateTime.UtcNow > activity.DueDate)
                return BadRequest("Resubmission deadline has passed.");

            // Update submission
            submission.PdfUrl = dto.PdfUrl;
            submission.SubmissionDate = DateTime.UtcNow;
            submission.StudentComment = dto.StudentComment;

            _context.Submissions.Update(submission);
            await _context.SaveChangesAsync();

            return Ok("Resubmission successful.");
        }

    }
}
