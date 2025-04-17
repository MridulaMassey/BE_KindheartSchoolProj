using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_App_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignGrade([FromBody] ActivityGradeDto activityGradeDto)
        {
            await _gradeService.AssignGradeToActivity(activityGradeDto);
            return Ok("Grade assigned to activity.");
        }

        [HttpPost("calculate-final")]
        public async Task<IActionResult> CalculateFinalGrade([FromBody] FinalGradeDto finalGradeDto)
        {
            double finalGrade = await _gradeService.CalculateFinalGrade(finalGradeDto);
            return Ok(new { FinalScore = finalGrade });
        }

        [HttpPost("release-final")]
        public async Task<IActionResult> ReleaseFinalGrade([FromBody] FinalGradeDto finalGradeDto)
        {
            bool success = await _gradeService.ReleaseFinalGrade(finalGradeDto);
            return success ? Ok("Final grade released.") : BadRequest("Grade release failed.");
        }
    }
}
