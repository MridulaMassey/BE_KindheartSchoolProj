using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IUserService
    {
        //create
        Task<string> RegisterUserAsync(string username, string email, string password, string roleName,string firstName,string lastName,Guid? classgroupId);
        
        //Read
        Task<ApplicationUser> GetProfileAsync(string username);
           

        // Update
        // Task<bool> UpdateUserAsync(string id, ApplicationUser updatedUser);
        Task<bool> UpdateUserAsync(string id, ProfileDTO updatedProfile);

        // Delete
        Task<bool> DeleteUserAsync(string id);

    }
}
