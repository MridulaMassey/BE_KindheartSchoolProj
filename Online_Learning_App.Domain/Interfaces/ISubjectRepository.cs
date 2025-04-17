using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App.Domain.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Subject> GetSubjectByIdAsync(Guid id);
        Task<IEnumerable<Subject>> GetAllAsync();
        Task AddAsync(Subject subject);
        Task UpdateAsync(Subject activity);
        Task DeleteAsync(Guid id);
    }
}
