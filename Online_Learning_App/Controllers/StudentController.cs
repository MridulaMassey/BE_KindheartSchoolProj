using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Infrastructure;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_APP.Application.DTO;


namespace Online_Learning_App_Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("update-class")]
        public async Task<IActionResult> UpdateStudentClass([FromBody] UpdateStudentClassDto dto)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserName == dto.UserName);

            if (student == null)
                return NotFound("Student not found");

            student.ClassLevel = dto.ClassLevel;
            student.ClassGroupId = dto.ClassGroupId;

            await _context.SaveChangesAsync();

            return Ok("Student updated successfully.");
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetStudent(string username)
        {
            var student = await _context.Students
                .Include(s => s.ClassGroup)
                .FirstOrDefaultAsync(s => s.UserName == username);

            if (student == null)
                return NotFound("Student not found");

            return Ok(new
            {
                student.UserName,
                student.Email,
                student.ClassLevel,
                student.ClassGroupId,
                ClassGroupName = student.ClassGroup?.ClassName
            });
        }

        [HttpGet("{username}/activities-with-submission")]
        public async Task<IActionResult> GetStudentActivitiesWithSubmissions(string username)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserName == username);

            if (student == null) return NotFound("Student not found.");

            //var activities = await _context.Activities
            //    .Where(a => a.ClassGroupId == student.ClassGroupId)
            //    .ToListAsync();

           // var activitiesclassGrooupSubject = await _context.ClassGroupSubject
           //.Where(a => a.ClassGroupId == student.ClassGroupId).Select(a=> a.ClassGroupSubjectId).FirstOrDefaultAsync();

            var activitiesStudent = await _context.ClassGroupSubjectStudentActivity.Include(a=>a.Activity)
                .Where(a => a.StudentId == student.Id)
                .ToListAsync();

            var submissions = await _context.Submissions
                .Where(s => s.StudentId == student.Id)
                .ToListAsync();

            var result = activitiesStudent.Select(a =>
            {
                var submission = submissions.FirstOrDefault(s => s.ActivityId == a.ActivityId);

                return new StudentActivityWithSubmissionDto
                {
                    ActivityId = a.ActivityId,
                    Title = a.Activity.Title,
                    Description = a.Activity.Description,
                    DueDate = a.Activity.DueDate,
                    PdfUrl = a.pdfUrl,
                    IsSubmitted = submission != null,
                    SubmissionUrl = submission?.PdfUrl,
                    SubmissionDate = submission?.SubmissionDate,
                    Feedback = submission?.Feedback,
                    Grade = submission?.Grade,
                    StudentComment = submission?.StudentComment

                };
            }).ToList();

            return Ok(result);
        }

        [HttpGet("/pendingsubmission")]
        public async Task<IActionResult> PendingSubmissions()
        {
            //var student = await _context.Students
            //    .FirstOrDefaultAsync(s => s.UserName == username);

            //if (student == null) return NotFound("Student not found.");

            //var activities = await _context.Activities
            //    .Where(a => a.ClassGroupId == student.ClassGroupId)
            //    .ToListAsync();

            // var activitiesclassGrooupSubject = await _context.ClassGroupSubject
            //.Where(a => a.ClassGroupId == student.ClassGroupId).Select(a=> a.ClassGroupSubjectId).FirstOrDefaultAsync();

            var activitiesStudent = await _context.ClassGroupSubjectStudentActivity.Include(a => a.Activity)
               // .Where(a => a.StudentId == student.Id)
                .ToListAsync();

            var submissions = await _context.Submissions
               // .Where(s => !string.IsNullOrEmpty( s.Feedback))
                .ToListAsync();

            var result = activitiesStudent.Select(a =>
            {
                var submission = submissions.FirstOrDefault(s => s.ActivityId == a.ActivityId);

                return new StudentActivityWithSubmissionDto
                {
                    ActivityId = a.ActivityId,
                    Title = a.Activity.Title,
                    Description = a.Activity.Description,
                    DueDate = a.Activity.DueDate,
                    PdfUrl = a.pdfUrl,
                    IsSubmitted = submission != null,
                    SubmissionUrl = submission?.PdfUrl,
                    SubmissionDate = submission?.SubmissionDate,
                    Feedback = submission?.Feedback,
                    Grade = submission?.Grade,
                    StudentComment = submission?.StudentComment

                };
            }).ToList();
            //var finalresult= result.Where(a=> a.PdfUrl==null).ToList();
            return Ok(result);
        }


        [HttpGet("get-student-id/{username}")]
        public async Task<IActionResult> GetStudentIdByUsername(string username)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserName == username);

            if (student == null)
                return NotFound("Student not found.");

            return Ok(new { studentId = student.Id });
        }

    }

}