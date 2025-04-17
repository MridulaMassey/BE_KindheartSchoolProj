using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_App_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassGroupSubjectController : ControllerBase
    {
        private readonly IClassGroupSubjectService _classGroupSubjectService;

        public ClassGroupSubjectController(IClassGroupSubjectService classGroupSubjectService)
        {
            _classGroupSubjectService = classGroupSubjectService;
        }

        // Create a ClassGroupSubject
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassGroupSubjectDto classGroupSubject)
        {
            if (classGroupSubject == null)
                return BadRequest("Invalid data.");

            var createdClassGroupSubject = await _classGroupSubjectService.AddClassGroupSubjectAsync(classGroupSubject);
            return CreatedAtAction(nameof(GetById), new { id = createdClassGroupSubject.ClassGroupSubjectId }, createdClassGroupSubject);
        }

        // Get a ClassGroupSubject by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var classGroupSubject = await _classGroupSubjectService.GetClassGroupSubjectByIdAsync(id);
            if (classGroupSubject == null)
                return NotFound($"ClassGroupSubject with ID {id} not found.");

            return Ok(classGroupSubject);
        }

        // Get all ClassGroupSubjects
        [HttpGet("classgroupslist")]
        public async Task<IActionResult> GetAll()
        {
            var classGroupSubjects = await _classGroupSubjectService.GetAllClassGroupSubjectsAsync();
            return Ok(classGroupSubjects);
        }

        // Get ClassGroupSubjects by ClassGroupId
        [HttpGet("by-class-group/{classGroupId}")]
        public async Task<IActionResult> GetByClassGroupId(Guid classGroupId)
        {
            var classGroupSubjects = await _classGroupSubjectService.GetByClassGroupIdAsync(classGroupId);
            return Ok(classGroupSubjects);
        }

        // Get ClassGroupSubjects by SubjectId
        [HttpGet("by-subject/{subjectId}")]
        public async Task<IActionResult> GetBySubjectId(Guid subjectId)
        {
            var classGroupSubjects = await _classGroupSubjectService.GetBySubjectIdAsync(subjectId);
            return Ok(classGroupSubjects);
        }

        // Update a ClassGroupSubject
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ClassGroupSubjectDto classGroupSubject)
        {
            if (classGroupSubject == null || id != classGroupSubject.ClassGroupSubjectId)
                return BadRequest("Invalid data.");

            await _classGroupSubjectService.UpdateClassGroupSubjectAsync(classGroupSubject);
            return NoContent();
        }

        // Delete a ClassGroupSubject
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingClassGroupSubject = await _classGroupSubjectService.GetClassGroupSubjectByIdAsync(id);
            if (existingClassGroupSubject == null)
                return NotFound($"ClassGroupSubject with ID {id} not found.");

            await _classGroupSubjectService.DeleteClassGroupSubjectAsync(id);
            return NoContent();
        }
    }
}
