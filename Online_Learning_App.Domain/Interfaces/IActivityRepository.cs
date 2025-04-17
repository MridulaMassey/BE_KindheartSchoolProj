using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App.Domain.Interfaces
{
    public interface IActivityRepository
    {
        Task<Activity> GetByIdAsync(Guid id);
        Task<IEnumerable<Activity>> GetAllAsync();
        Task AddAsync(Activity activity);
        Task UpdateAsync(Activity activity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Activity>> GetBySubjectAndClassAsync(Guid subjectId, Guid classId);
    }
}
