using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<ApplicationUser> GetUserByUsernameAsync(string username);
        Task UpdateAsync(ApplicationUser existingUser);
        Task DeleteAsync(ApplicationUser existingUser);
        Task<bool> ValidatePasswordAsync(ApplicationUser user, string password);

    }
}
