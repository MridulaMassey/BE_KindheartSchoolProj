using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_App.Infrastructure;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using System.Threading.Tasks;

namespace AuthenticationApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<ApplicationUser> userManager, 
            RoleManager<Role> roleManager, 
            ApplicationDbContext context, IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<string> RegisterUserAsync(string username, 
            string email, string password, 
            string roleName,string firstName, 
            string lastName,Guid? ClassgroupID)
        {
            // Check if the role exists
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return "Role does not exist!";
            }

            // Create a new user
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                RoleId= role.Id,
                FirstName=firstName,
                LastName=lastName,
            };

            // Create the user with hashed password
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                //   return "User creation failed!";
                return string.Join("; ", result.Errors.Select(e => e.Description));
            }

            // Assign the role to the user
          
            await _userManager.AddToRoleAsync(user, roleName);
            //var students = new Student
            //{
            //    Id = new Guid(),  // This links the Student to the ApplicationUser
            //    Email = user.Email,
            //    UserName = user.UserName,
            //    ClassLevel = "One",
            //    UserId=user.Id
            //};
            //_context.Students.Attach(students);
            //       await _context.SaveChangesAsync();
            // Check if the user already exists as a Student or Teacher
            // **Detach the user to prevent EF tracking issues**
            _context.Entry(user).State = EntityState.Detached;
            if (role.Name == "Student")
            {
                // Check if the student already exists
                var existingStudent = await _context.Students
                    .FirstOrDefaultAsync(s => s.Id == user.Id);

                //  var existingStudent = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == user.Id);
                if (existingStudent == null)  // Only add if the student doesn't already exist
                {
                    var student = new Student
                    {
                        Id = Guid .NewGuid(),
                        // Id = user.Id,  // This links the Student to the ApplicationUser
                        Email = user.Email,
                        UserName = user.UserName,
                        ClassLevel = "TBA",
                        UserId = user.Id,
                        ClassGroupId= ClassgroupID
                        //UserId=user.Id

                    };
             
                    _context.Students.Add(student);  // Add to the Students table

                }
            }
           else if (role.Name == "Teacher")
            {
                // Check if the teacher already exists
                var existingTeacher = await _context.Teachers
                    .FirstOrDefaultAsync(t => t.Id == user.Id);

                if (existingTeacher == null)  // Only add if the teacher doesn't already exist
                {
                    var teacher = new Teacher
                    {
                        Id = new Guid(),  // This links the Teacher to the ApplicationUser
                        Email = user.Email,
                        UserName = user.UserName,
                        UserId = user.Id
                    };
                    _context.Teachers.Add(teacher);  // Add to the Teachers table
                }
            }
            else if (role.Name == "Admin")
            {
                // Check if the teacher already exists
                var existingTeacher = await _context.Teachers
                    .FirstOrDefaultAsync(t => t.Id == user.Id);

                if (existingTeacher == null)  // Only add if the teacher doesn't already exist
                {
                    var admin = new Admin
                    {
                        AdminId = new Guid(),  // This links the Teacher to the ApplicationUser
                        Email = user.Email,
                        UserName = user.UserName,
                        UserId = user.Id
                    };
                    _context.Admin.Add(admin);  // Add to the Teachers table
                }
            }
            // Ensure your ApplicationUser entity has a RoleId property
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password); // Correct hashing

          //  await _userManager.UpdateAsync(user);
            // Save all changes to the database
           await _context.SaveChangesAsync();

            return "User registered successfully!";
        }

        public async Task<ApplicationUser> GetProfileAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<bool> UpdateUserAsync(string id, ProfileDTO updatedProfile)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return false;

            existingUser.FirstName = updatedProfile.FirstName;
            existingUser.LastName = updatedProfile.LastName;
            existingUser.Email = updatedProfile.Email;
            existingUser.UserName = updatedProfile.userName;
            //existingUser.RoleId = updatedProfile.RoleId;
            existingUser.RoleId = Guid.TryParse(updatedProfile.RoleId, out var roleGuid)
                ? roleGuid
                : (Guid?)null;
            existingUser.ClassgroupId = updatedProfile.ClassgroupId;

            await _userRepository.UpdateAsync(existingUser);
            return true;
        }

        public async Task<bool> DeleteUserAsync(String id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return false;

            await _userRepository.DeleteAsync(existingUser);
            return true;
        }



    }
}
