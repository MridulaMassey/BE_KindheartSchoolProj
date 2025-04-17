using System.Data;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_App.Domain.Entities;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_App_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;

        }

        //To Profile using "userName"
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var profile = await _userService.GetProfileAsync(username);
            if (profile == null)
                return NotFound(new { message = "Profile not found" });

            return Ok(new { message = "Profile retrieved successfully", profile });
        }

        //API to Update Proile  using ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(string id, [FromBody] ProfileDTO model)
        {
            var result = await _userService.UpdateUserAsync(id, model);
            if (!result)
                return NotFound(new { message = "Update failed. Profile not found." });

            return Ok(new { message = "Profile updated successfully" });

        }

        // Delete a profile by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound(new { message = "Delete failed. Profile not found." });

            return Ok(new { message = "Profile deleted successfully" });
        }


    }
}
