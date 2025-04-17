using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App.Domain.Interfaces
{
    public interface IClassGroupSubjectStudentActivityRepository
    {
        Task AddAsync(ClassGroupSubjectStudentActivity classgroupSubjectStudentActivity);
        Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetAllAsync();
        Task UpdateAsync(ClassGroupSubjectStudentActivity classgroupSubjectStudentActivity);
        Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClassGroupSubjectActivityByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
