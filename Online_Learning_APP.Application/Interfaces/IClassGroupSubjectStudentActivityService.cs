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
        Task<ClassGroupSubjectStudentActivityDto> UpdateSubjectAsync(Guid activityId, ClassGroupSubjectStudentActivityDto updateSubjectDto);
        Task<bool> DeleteSubjectAsync(Guid subjectId);
    }
}
