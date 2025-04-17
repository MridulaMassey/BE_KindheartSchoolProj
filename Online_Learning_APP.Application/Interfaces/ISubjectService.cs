using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync();
        Task<SubjectDto> GetSubjectByIdAsync(Guid subjectId);
        Task<Guid> CreateSubjectAsync(SubjectDto subjectDto);
        Task<SubjectDto> UpdateSubjectAsync(Guid activityId, UpdateSubjectDto updateSubject);
 
         Task<bool> DeleteSubjectAsync(Guid subjectId);
    }
}
