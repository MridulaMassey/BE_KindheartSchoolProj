using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IAuthService
    {
        //Task<bool> AuthenticateUserAsync(LoginDTO loginDto);
        Task<(bool IsAuthenticated, string? Role)> AuthenticateUserAsync(LoginDTO loginDto);
    }
}
