using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;
using Online_Learning_App.Domain.Entities;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace Online_Learning_APP.Application.Services
{
    public class ClassGroupService : IClassGroupService
    {
        private readonly ApplicationDbContext _context;

        public ClassGroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClassGroup> CreateClassGroupAsync(ClassGroupCreateDto classGroupDto)
        {
            try
            { 
            // Validate that the admin exists
            // First, find the admin using AdminId
            var admin = await _context.Admin.FindAsync(classGroupDto.AdminId);
            if (admin == null)
                throw new Exception("Admin not found");

            // Then, create the ClassGroup instance and initialize it
            var classGroup = new ClassGroup
            {
                ClassGroupId = Guid.NewGuid(),
                ClassName = classGroupDto.ClassName,
                AdminId = classGroupDto.AdminId,
                // Do not initialize Activities and ClassGroupSubjects yet
            };

            // Populate Activities and ClassGroupSubjects
            //classGroup.Activities = await _context.Activities
            //                                      .Where(a => classGroupDto.ActivityIds.Contains(a.Id))
            //                                      .ToListAsync();

            //// Populate the many-to-many relationship with Subjects
            //classGroup.ClassGroupSubjects = classGroupDto?.SubjectIds
            //    .Select(subjectId => new ClassGroupSubject
            //    {
            //        ClassGroupId = classGroup.ClassGroupId,
            //        SubjectId = subjectId
            //    }).ToList();
            //  classGroup.ClassGroupSubjects = null;

            // Save the classGroup object in the context (database)
            await _context.ClassGroups.AddAsync(classGroup);
            await _context.SaveChangesAsync();

            return classGroup;
        }

             catch (DbUpdateException dbEx)
    {
                // Handle database-specific exceptions (e.g., unique constraint violations)
                throw new Exception("A database error occurred while creating the class group.", dbEx);
            }
    catch (Exception ex)
    {
                // General error fallback
                throw new Exception("An error occurred while creating the class group.", ex);
            }
        }
        public async Task<List<ClassGroup>> GetAllClassGroupsAsync()
        {
            return await _context.ClassGroups.ToListAsync();
        }


        public async Task<IEnumerable<StudentDto>> GetStudentsByClassGroupIdAsync(Guid classGroupId)
        {
            var students = await _context.Students
                .Where(s => s.ClassGroupId == classGroupId)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    UserName = s.UserName,
                    Email = s.Email,
                    ClassLevel = s.ClassLevel
                })
                .ToListAsync();

            return students;
        }

        public async Task<bool> AssignTeacherToClassGroupAsync(Guid classGroupId, Guid teacherId)
        {
            var classGroup = await _context.ClassGroups.FindAsync(classGroupId);
            if (classGroup == null) return false;

            var teacher = await _context.Teachers.FindAsync(teacherId);
            if (teacher == null) return false;

            classGroup.TeacherId = teacherId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TeacherLoadDto> GetTeacherLoadAsync(Guid teacherId)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null) return null;

            var classGroups = await _context.ClassGroups
                .Where(cg => cg.TeacherId == teacherId)
                .ToListAsync();

            return new TeacherLoadDto
            {
                TeacherId = teacher.Id,
                UserName = teacher.UserName,
                Email = teacher.Email,
                ClassGroupCount = classGroups.Count,
                ClassGroupNames = classGroups.Select(cg => cg.ClassName).ToList()
            };
        }

        //public async Task<List<ClassGroupSummaryDto>> GetClassGroupsByTeacherIdAsync(Guid teacherId)
        //{
        //    var classGroups = await _context.ClassGroups
        //        .Include(cg => cg.Students)
        //        .Where(cg => cg.TeacherId == teacherId)
        //        .ToListAsync();

        //    var result = classGroups.Select(g => new ClassGroupSummaryDto
        //    {
        //        ClassGroupId = g.ClassGroupId,
        //        ClassName = g.ClassName,
        //        StudentCount = g.Students?.Count ?? 0
        //    }).ToList();

        //    return result;
        //}

    }
}
