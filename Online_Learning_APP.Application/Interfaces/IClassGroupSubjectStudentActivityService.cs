using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IClassGroupSubjectStudentActivityService
    {
        Task<Guid> CreateSubjectAsync(ClassGroupSubjectStudentActivityDto classgroupsubjectStudentActivityDto);
        Task<IEnumerable<ClassGroupSubjectStudentActivityDto>> GetSubjectByIdAsync(Guid subjectId);
        Task<IEnumerable<ClassGroupSubjectStudentActivityDto>> GetAllSubjectsAsync();
        Task<ClassGroupSubjectStudentActivityDto> UpdateSubjectAsync(UpdateClassGroupSubjectStudentActivityDto updateSubjectDto);
        Task<IEnumerable<NotificationDto>> GetClassGroupByStudentByIdAsync(GetClassGroupSubStudActivityDto clg);
        Task<bool> DeleteSubjectAsync(Guid subjectId);
        Task<ClassGroupSubjectStudentActivityDto> UpdateIsProcessedAsync(updateNotificationDto updateSubject);
    }
}
