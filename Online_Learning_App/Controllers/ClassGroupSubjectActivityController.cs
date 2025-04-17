using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App_Presentation.Controllers
{
    // Controller Layer
    [Route("api/[controller]")]
    [ApiController]
    public class ClassGroupSubjectActivityController : ControllerBase
    {
        private readonly IClassGroupSubjectActivityService _service;

        public ClassGroupSubjectActivityController(IClassGroupSubjectActivityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => 
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClassGroupSubjectActivity entity)
        {
            var createdEntity = await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.ClassGroupSubjectActivityId }, createdEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ClassGroupSubjectActivity entity)
        {
            if (id != entity.ClassGroupSubjectActivityId) return BadRequest();
            return Ok(await _service.UpdateAsync(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }

}
