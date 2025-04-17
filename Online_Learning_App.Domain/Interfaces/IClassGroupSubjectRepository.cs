using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App.Domain.Interfaces
{
    public interface IClassGroupSubjectRepository
    {
        Task AddAsync(ClassGroupSubject classGroupSubject);
        Task<ClassGroupSubject> GetByIdAsync(Guid id);

        Task<IEnumerable<ClassGroupSubject>> GetAllAsync();
        Task<IEnumerable<ClassGroupSubject>> GetByClassGroupIdAsync(Guid classGroupId);
       Task<IEnumerable<ClassGroupSubject>> GetBySubjectIdAsync(Guid subjectId);

       Task UpdateAsync(ClassGroupSubject classGroupSubject);
        Task DeleteAsync(Guid id);
    }
}
