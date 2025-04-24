using AuthenticationApp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDTO model)
        //{
        //    var (isAuthenticated, role) = await _authService.AuthenticateUserAsync(model);
        //    //AuthenticateTokenUserAsync
        //    if (!isAuthenticated)
        //    {
        //        return Unauthorized(new { message = "Invalid credentials" ,role=role});
        //    }

        //    return Ok(new { message = "Login successful",Rolename= role });
        //}


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var isAuthenticated= await _authService.AuthenticateTokenUserAsync(model);
            //AuthenticateTokenUserAsync
            if (!string.IsNullOrEmpty(isAuthenticated.Token)&& string.IsNullOrEmpty(isAuthenticated.Role))
            {
                return Unauthorized(new { message = "Invalid credentials", role = isAuthenticated.Role });
            }

            return Ok(new { message = "Login successful", Rolename = isAuthenticated.Role,Token= isAuthenticated.Token });
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            var result = await _userService.RegisterUserAsync(dto.UserName, dto.Email, dto.Password, dto.Role,dto.FirstName,dto.LastName,dto.ClassGroupId);
            return Ok(new { message = result });
        }
        [HttpGet("test-broadcast")]
        public async Task<IActionResult> TestBroadcast([FromServices] IHubContext<ActivityHub> hub)
        {
            await hub.Clients.All.SendAsync("ReceiveActivity", new { Test = "Hello from test!" });
            return Ok("Sent");
        }


        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
        {
            var result = await _roleService.CreateRoleAsync(dto.RoleName);
            return Ok(new { message = result });
        }
    }
}
