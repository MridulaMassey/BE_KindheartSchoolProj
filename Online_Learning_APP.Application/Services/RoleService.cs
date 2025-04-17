using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Online_Learning_App.Domain.Entities;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_APP.Application.Services
{
    public class RoleService: IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<string> CreateRoleAsync(string roleName)
        {
            // Check if the role already exists
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return "Role already exists!";
            }

            // Create and store the role
            var role = new Role
            {
               /* Id = Guid.NewGuid(),*/ // Explicitly set the Id
                Name = roleName,
                NormalizedName = roleName.ToUpper() // Identity framework requires NormalizedName
            };
            var result = await _roleManager.CreateAsync(role);

            return result.Succeeded ? "Role created successfully!" : "Failed to create role.";
        }
    }
}
