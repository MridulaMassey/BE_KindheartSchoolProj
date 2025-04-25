using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Infrastructure;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_App_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITeacherService _teacherService;

        public TeacherController(ApplicationDbContext context, ITeacherService teacherService)
        {
            _context = context;
            _teacherService = teacherService;
        }

        //Get api/Teacher/get-teacher-id/{}
        [HttpGet("get-teacher-id/{username}")]
        public async Task<IActionResult> GetTeacherIdByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
                return NotFound("User Not Found");

            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.UserId == user.Id);
            if (teacher == null)
                return NotFound("Teacher Not Found");

            return Ok(new { TeacherId = teacher.Id });

        }

        [HttpGet("{username}/students")]
        public async Task<ActionResult<List<ClassGroupWithStudentsDto>>> GetTeacherStudents(string username)
        {
            var data = await _teacherService.GetTeacherClassStudentsAsync(username);

            if (data == null || data.Count == 0)
                return NotFound("No class group or students found for this teacher.");

            return Ok(data);
        }

    }
}