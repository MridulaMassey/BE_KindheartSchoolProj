using Online_Learning_APP.Application.DTO;
using Online_Learning_App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Online_Learning_APP.Application.Services
{
    namespace Online_Learning_APP.Services
    {
        public class TeacherService : ITeacherService
        {
            private readonly ApplicationDbContext _context;

            public TeacherService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<ClassGroupWithStudentsDto>> GetTeacherClassStudentsAsync(string teacherUsername)
            {
                var teacher = await _context.Teachers
                    .FirstOrDefaultAsync(t => t.UserName == teacherUsername);

                if (teacher == null)
                    return new List<ClassGroupWithStudentsDto>();

                var classGroups = await _context.ClassGroups
                    .Where(cg => cg.TeacherId == teacher.Id)
                    .ToListAsync();

                var result = new List<ClassGroupWithStudentsDto>();

                foreach (var group in classGroups)
                {
                    var students = await _context.Students
                        .Where(s => s.ClassGroupId == group.ClassGroupId)
                        .ToListAsync();

                    var studentUserIds = students.Select(s => s.UserId).ToList();

                    var studentProfiles = await _context.Users
                        .Where(u => studentUserIds.Contains(u.Id))
                        .Select(u => new StudentProfileDto
                        {
                            UserId = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email
                        }).ToListAsync();

                    result.Add(new ClassGroupWithStudentsDto
                    {
                        ClassGroupId = group.ClassGroupId,
                        ClassName = group.ClassName,
                        Students = studentProfiles
                    });
                }

                return result;
            }
        }
    }
}
