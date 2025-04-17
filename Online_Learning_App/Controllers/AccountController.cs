using AuthenticationApp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_APP.Application.Services;

namespace Online_Learning_App.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AccountController(IAuthService authService, IUserService userService, IRoleService roleService)
        {
            _authService = authService;
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var (isAuthenticated, role) = await _authService.AuthenticateUserAsync(model);
            if (!isAuthenticated)
            {
                return Unauthorized(new { message = "Invalid credentials" ,role=role});
            }

            return Ok(new { message = "Login successful",Rolename= role });
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            var result = await _userService.RegisterUserAsync(dto.UserName, dto.Email, dto.Password, dto.Role,dto.FirstName,dto.LastName,dto.ClassGroupId);
            return Ok(new { message = result });
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
        {
            var result = await _roleService.CreateRoleAsync(dto.RoleName);
            return Ok(new { message = result });
        }
    }
}
