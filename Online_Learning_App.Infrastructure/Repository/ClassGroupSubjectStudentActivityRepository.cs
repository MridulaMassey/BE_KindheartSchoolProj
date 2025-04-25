using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;

namespace Online_Learning_App.Infrastructure.Repository
{
    public class ClassGroupSubjectStudentActivityRepository: IClassGroupSubjectStudentActivityRepository
    {
        private readonly ApplicationDbContext _dbContext; // Replace with your DbContext
        public ClassGroupSubjectStudentActivityRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(ClassGroupSubjectStudentActivity classgroupSubjectStudentActivity)
        {
            try
            { 
            await _dbContext.ClassGroupSubjectStudentActivity.AddAsync(classgroupSubjectStudentActivity);
            await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
        public async Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClassGroupSubjectActivityByIdAsync(Guid id)
        {
            return await _dbContext.ClassGroupSubjectStudentActivity.Where(cgs => cgs.ActivityId == id).Include(a => a.Student).Include(a => a.Activity).ThenInclude(a=>a.Teacher).ThenInclude(a=>a.User).Include(a => a.ClassGroupSubject).ThenInclude(a => a.ClassGroup).ToListAsync(); 
        }
        public async Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClassGroupSubjectActivityByStudentIdAsync(Guid id,Guid StudentID)
        {
            return await _dbContext.ClassGroupSubjectStudentActivity.Where(cgs => cgs.ActivityId == id && cgs.StudentId==StudentID).Include(a => a.Student).Include(a => a.Activity).ThenInclude(a => a.Teacher).ThenInclude(a => a.User).Include(a => a.ClassGroupSubject).ThenInclude(a => a.ClassGroup).ToListAsync();
        }
        public async Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClassGroupSubjectActivityStudentByIdAsync(Guid id,Guid studentID)
        {
            return await _dbContext.ClassGroupSubjectStudentActivity.Where(cgs => cgs.ActivityId == id && cgs.StudentId==studentID).Include(a => a.Student).Include(a => a.Activity).ThenInclude(a => a.Teacher).ThenInclude(a => a.User).Include(a => a.ClassGroupSubject).ThenInclude(a => a.ClassGroup).ToListAsync();
        }

        public async Task<ClassGroupSubjectStudentActivity> GetActivitySubjectStudentByIdAsync(Guid id, Guid studentID)
        {
            return await _dbContext.ClassGroupSubjectStudentActivity.Where(cgs => cgs.ActivityId == id && cgs.StudentId == studentID).Include(a => a.Student).Include(a => a.Activity).ThenInclude(a => a.Teacher).ThenInclude(a => a.User).Include(a => a.ClassGroupSubject).ThenInclude(a => a.ClassGroup).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetAllAsync()
        {
            return await _dbContext.ClassGroupSubjectStudentActivity.Include(a=>a.Student).Include(a=> a.Activity).Include(a => a.ClassGroupSubject).ThenInclude(a=> a.ClassGroup).ToListAsync();
        }
        public async Task UpdateAsync(ClassGroupSubjectStudentActivity classgroupSubjectStudentActivity)
        {
            _dbContext.ClassGroupSubjectStudentActivity.Update(classgroupSubjectStudentActivity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> UpdateISProcessedAsync(ClassGroupSubjectStudentActivity classgroupSubjectStudentActivity)
        {
            try
            {
                var existingEntity = await _dbContext.ClassGroupSubjectStudentActivity
         .FirstOrDefaultAsync(x =>
             x.ActivityId == classgroupSubjectStudentActivity.ActivityId &&
             x.StudentId == classgroupSubjectStudentActivity.StudentId &&
             x.ClassGroupSubjectStudentActivityId == classgroupSubjectStudentActivity.ClassGroupSubjectStudentActivityId);

                if (existingEntity == null)
                {
                    Console.WriteLine("Entity not found for the given keys.");
                    return false;
                }
                existingEntity.IsProcessed = classgroupSubjectStudentActivity.IsProcessed;
             //   _dbContext.ClassGroupSubjectStudentActivity.Update(classgroupSubjectStudentActivity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency exception
                Console.WriteLine("Concurrency exception: " + ex.Message);
                return false;
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception (e.g., constraint violations)
                Console.WriteLine("Database update exception: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine("Unexpected error during update: " + ex.Message);
                return false;
            }
        }
        public async Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClgActivityStudentByIdAsync(Guid id, Guid studentID)
        {
            return await _dbContext.ClassGroupSubjectStudentActivity.Where(cgs => cgs.StudentId == studentID).Include(a => a.Student).Include(a => a.Activity).ThenInclude(a => a.Teacher).ThenInclude(a => a.User).Include(a => a.ClassGroupSubject).ThenInclude(a => a.ClassGroup).ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var subject = await _dbContext.ClassGroupSubjectStudentActivity.FindAsync(id);
            if (subject != null)
            {
                _dbContext.ClassGroupSubjectStudentActivity.Remove(subject);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
