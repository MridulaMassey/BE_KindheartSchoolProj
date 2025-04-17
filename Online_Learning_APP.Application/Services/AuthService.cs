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

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(IUserRepository userRepository, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        //public async Task<bool> AuthenticateUserAsync(LoginDTO loginDto)
        //{
        //    var user = await _userRepository.GetUserByUsernameAsync(loginDto.UserName);
        //    if (user == null) return false;
        //    // string base64Encoded = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(loginDto.PasswordHash));
        // //   loginDto.PasswordHash = _userManager.PasswordHasher.HashPassword(user,loginDto.PasswordHash);
        //  //  user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, "India@123"); // Correct hashing
        // //   await _userManager.UpdateAsync(user);
        //    var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

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
    }


}