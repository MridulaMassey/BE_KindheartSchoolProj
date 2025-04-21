using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_App_Presentation.Controllers
{
    [Route("api/classgroups")]
    [ApiController]
    public class ClassGroupController : ControllerBase
    {
        private readonly IClassGroupService _classGroupService;

        public ClassGroupController(IClassGroupService classGroupService)
        {
            _classGroupService = classGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassGroup([FromBody] ClassGroupCreateDto classGroupDto)
        {
            if (classGroupDto == null || string.IsNullOrWhiteSpace(classGroupDto.ClassName))
            {
                return BadRequest("Invalid class group data.");
            }

            try
            {
                var classGroup = await _classGroupService.CreateClassGroupAsync(classGroupDto);
                return Ok(classGroup);
                // return CreatedAtAction(nameof(classGroup), new { id = classGroup.ClassGroupId }, classGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClassGroups()
        {
            var classGroups = await _classGroupService.GetAllClassGroupsAsync();

            if (classGroups == null || !classGroups.Any())
                return NotFound();

            return Ok(classGroups);
        }


        [HttpGet("{classGroupId}/students")]
        public async Task<IActionResult> GetStudentsByClassGroup(Guid classGroupId)
        {
            var students = await _classGroupService.GetStudentsByClassGroupIdAsync(classGroupId);

            if (students == null || !students.Any())
                return NotFound("No students found in this class group.");

            return Ok(students);
        }

        [HttpPut("assign-teacher")]
        public async Task<IActionResult> AssignTeacherToClassGroup([FromBody] AssignTeacherDto dto)
        {
            var success = await _classGroupService.AssignTeacherToClassGroupAsync(dto.ClassGroupId, dto.TeacherId);

            if (!success)
                return NotFound("ClassGroup or Teacher not found.");

            return Ok("Teacher assigned to class group successfully.");
        }

        [HttpGet("teacher-load/{teacherId}")]
        public async Task<IActionResult> GetTeacherLoad(Guid teacherId)
        {
            var load = await _classGroupService.GetTeacherLoadAsync(teacherId);

            if (load == null)
                return NotFound("Teacher not found.");

            return Ok(load);
        }

        //[HttpGet("teacher-load/{teacherId}")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public async Task<IActionResult> GetClassGroupsByTeacher(Guid teacherId)
        //{
        //    var classGroupDtos = await _classGroupService.GetClassGroupsByTeacherIdAsync(teacherId);

        //    if (classGroupDtos == null || !classGroupDtos.Any())
        //        return NotFound("No class groups found for this teacher.");

        //    return Ok(classGroupDtos);
        //}
 

    }
}
