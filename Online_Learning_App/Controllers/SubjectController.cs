using Microsoft.AspNetCore.Mvc;
using Online_Learning_App.Application.Services;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Online_Learning_App_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectDto subjectDto)
        {
            if (subjectDto == null || string.IsNullOrWhiteSpace(subjectDto.SubjectName))
            {
                return BadRequest("Invalid subject data.");
            }

            var subjectId = await _subjectService.CreateSubjectAsync(subjectDto);
            return Ok(new { message = subjectId.ToString() });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubjectById(Guid id)
        {
            var subjectDto = await _subjectService.GetSubjectByIdAsync(id);
            if (subjectDto == null)
            {
                return NotFound(new { message = "Subject not found" });
            }
            return Ok(subjectDto);
        }

        [HttpGet("subjectslist")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<SubjectDto>> UpdateSubject(Guid id, [FromBody] UpdateSubjectDto updateSubjectDto)
        {
            var updatedSubject = await _subjectService.UpdateSubjectAsync(id, updateSubjectDto);
            if (updatedSubject == null)
            {
                return NotFound(new { message = "Subject not found" });
            }
            return Ok(updatedSubject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubject(Guid id)
        {
            var deleted = await _subjectService.DeleteSubjectAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = "Subject not found" });
            }
            return NoContent(); // 204 - Successfully deleted
        }

    }
}
