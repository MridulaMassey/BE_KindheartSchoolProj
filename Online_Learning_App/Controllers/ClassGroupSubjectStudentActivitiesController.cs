using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_App_Presentation.Controllers
{
    [ApiController]
    [Route("api/classgroupsubjectstudentactivities")]
    public class ClassGroupSubjectStudentActivitiesController : ControllerBase
    {
        private readonly IClassGroupSubjectStudentActivityService _service;
        //IClassGroupSubjectStudentActivityService

        public ClassGroupSubjectStudentActivitiesController(IClassGroupSubjectStudentActivityService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateClassGroupSubjectStudentActivity(ClassGroupSubjectStudentActivityDto classGroupSubjectStudentActivityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityId = await _service.CreateSubjectAsync(classGroupSubjectStudentActivityDto);
            return CreatedAtAction(nameof(GetClassGroupSubjectStudentActivityById), new { activityId = activityId }, activityId);
        }

        [HttpGet("{activityId}")]
        public async Task<ActionResult<ClassGroupSubjectStudentActivityDto>> GetClassGroupSubjectStudentActivityById(Guid activityId)
        {
            var activity = await _service.GetSubjectByIdAsync(activityId);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        [HttpPost("activitynotifications")]
        public async Task<ActionResult<NotificationDto>> ClGSubjectStudentActivityById(GetClassGroupSubStudActivityDto activityId)
        {
            var activity = await _service.GetNotificationClgByStudentByIdAsync(activityId);
            if (activity == null)
            {
                return NotFound();
            }
            var activityresult = activity.Where(a=> a.IsProcessed==false);
            return Ok(activityresult);
        }
        [HttpPost("teachersubmission")]
        public async Task<ActionResult<ClassGroupSubjectStudentActivityDto>> ClGStudentActivityByStudId(GetClassGroupSubStudActivityDto activityId)
        {
            var activity = await _service.GetClassGroupByStudentByIdAsync(activityId);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassGroupSubjectStudentActivityDto>>> GetAllClassGroupSubjectStudentActivities()
        {
            var activities = await _service.GetAllSubjectsAsync();
            return Ok(activities);
        }
        /// <summary>
        /// added isprocessed endpoint
        /// UpdateIsProcessedAsync
        /// </summary>
        /// <param name="updateActivityDto"></param>
        /// <returns></returns>
        /// 
        [HttpPut("{activityId}")]
        public async Task<IActionResult> UpdateClassGroupSubjectStudentActivity([FromBody] UpdateClassGroupSubjectStudentActivityDto updateActivityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updateActivityDto.ActivityId != updateActivityDto.ActivityId)
            {
                return BadRequest("Activity ID in the route does not match the ID in the request body.");
            }

            var updatedActivity = await _service.UpdateSubjectAsync(updateActivityDto);
            if (updatedActivity == null)
            {
                return NotFound();
            }

            return NoContent(); // Successful update, returns 204 No Content
        }


        [HttpPost("updateisprocessed")]
        public async Task<IActionResult> UpdateIsProccesdStudentActivity([FromBody] updateNotificationDto updateActivityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updateActivityDto.ActivityId != updateActivityDto.ActivityId)
            {
                return BadRequest("Activity ID in the route does not match the ID in the request body.");
            }

            var updatedActivity = await _service.UpdateIsProcessedAsync(updateActivityDto);
            if (updatedActivity == null)
            {
                return NotFound();
            }

            return NoContent(); // Successful update, returns 204 No Content
        }

        [HttpDelete("{activityId}")]
        public async Task<IActionResult> DeleteClassGroupSubjectStudentActivity(Guid activityId)
        {
            var deleted = await _service.DeleteSubjectAsync(activityId);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent(); // Successful deletion, returns 204 No Content
        }
    }
}
