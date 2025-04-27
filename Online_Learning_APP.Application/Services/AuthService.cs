using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
//using Application.DTOs;
//using Application.Interfaces;
using Online_Learning_App.Domain.Entities;
//using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        //public AuthService(IUserRepository userRepository, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        //{
        //    _userRepository = userRepository;
        //    _signInManager = signInManager;
        //    _httpContextAccessor = httpContextAccessor;
        //    _userManager = userManager;

        //}
        public AuthService(
    IUserRepository userRepository,
    SignInManager<ApplicationUser> signInManager,
    IHttpContextAccessor httpContextAccessor,
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _configuration = configuration;
        }



        //    //if (!signInResult.Succeeded)
        //    //{
        //    //    Console.WriteLine("Sign-in failed");
        //    //    return signInResult; // Return early if password does not match
        //    //}
        //    //var result = await _signInManager.PasswordSignInAsync(user, loginDto.PasswordHash, loginDto.RememberMe, false);
        //    return signInResult.Succeeded;
        //}
        public async Task<(bool IsAuthenticated, string? Role)> AuthenticateUserAsync(LoginDTO loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.UserName);
            if (user == null) return (false, null);

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!signInResult.Succeeded)
            {
                return (false, null);
            }

            // Fetch user roles (assuming one role per user)
            var roles = await _userManager.GetRolesAsync(user);
            string? role = roles.Count > 0 ? roles[0] : null;

            return (true, role);
        }

        public async Task<TokenDTO?> AuthenticateTokenUserAsync(LoginDTO loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.UserName);
            if (user == null) return null;

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!signInResult.Succeeded) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.Count > 0 ? roles[0] : "Student"; // Default to Student if none assigned

            // Get JWT settings
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            // Create claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, role)

    };

            var securityKey = new SymmetricSecurityKey(key);
            // Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = issuer,
                Audience = audience,
                //RoleClaimType = "role", // This tells it to read "role" from token
                //NameClaimType = "unique_name"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new TokenDTO
            {
                Token = tokenString,
                Role = role
            };
        }


    }


}