using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App.Domain.Interfaces
{
    public interface IClassGroupSubjectActivityRepository
    {
        Task<IEnumerable<ClassGroupSubjectActivity>> GetAllAsync();
        Task<ClassGroupSubjectActivity> GetByIdAsync(Guid id);
        Task<ClassGroupSubjectActivity> CreateAsync(ClassGroupSubjectActivity entity);
        Task<ClassGroupSubjectActivity> UpdateAsync(ClassGroupSubjectActivity entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
