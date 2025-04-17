using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.Services;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IRoleService
    {
        Task<string> CreateRoleAsync(string roleName);
    }
}
