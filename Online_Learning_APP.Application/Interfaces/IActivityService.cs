using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IActivityService
    {
        Task<ActivityDto> UpdateTeacherActivityAsync(UpdateTeacherSubmissionDto createActivity);
        Task<ActivityDto> CreateActivityAsync(CreateActivityDto createActivity);
        Task<ActivityDto> GetActivityByIdAsync(Guid activityId);
        Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync();
        Task<ActivityDto> UpdateActivityAsync(Guid activityId, UpdateActivityDto updateActivityDto);
        Task<bool> DeleteActivityAsync(Guid activityId);
        Task<List<Activity>> GetUpcomingActivitiesForStudent(Guid studentId);
    }
}
