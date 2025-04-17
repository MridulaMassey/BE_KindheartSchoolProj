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
            await _dbContext.ClassGroupSubjectStudentActivity.AddAsync(classgroupSubjectStudentActivity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClassGroupSubjectActivityByIdAsync(Guid id)
        {
            return await _dbContext.ClassGroupSubjectStudentActivity.Where(cgs => cgs.ActivityId == id).Include(a => a.Student).Include(a => a.Activity).Include(a => a.ClassGroupSubject).ThenInclude(a => a.ClassGroup).ToListAsync(); 
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
