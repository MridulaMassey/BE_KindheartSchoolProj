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
    public class ClassGroupSubjectRepository: IClassGroupSubjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClassGroupSubjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ClassGroupSubject classGroupSubject)
        {
            await _dbContext.ClassGroupSubject.AddAsync(classGroupSubject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ClassGroupSubject> GetByIdAsync(Guid id)
        {
            return await _dbContext.ClassGroupSubject
              .Include(cgs => cgs.ClassGroup)
               .Include(cgs => cgs.Subject).ThenInclude(a=> a.Activities)
                .FirstOrDefaultAsync(cgs => cgs.ClassGroupSubjectId == id);
        }

        public async Task<IEnumerable<ClassGroupSubject>> GetAllAsync()
        {
            return await _dbContext.ClassGroupSubject
               .Include(cgs => cgs.ClassGroup)
                .Include(cgs => cgs.Subject)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassGroupSubject>> GetByClassGroupIdAsync(Guid classGroupId)
        {
            return await _dbContext.ClassGroupSubject
                .Where(cgs => cgs.ClassGroupId == classGroupId)
                .Include(cgs => cgs.Subject)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassGroupSubject>> GetBySubjectIdAsync(Guid subjectId)
        {
            return await _dbContext.ClassGroupSubject
                .Where(cgs => cgs.SubjectId == subjectId)
                .Include(cgs => cgs.ClassGroup).ThenInclude(a => a.Activities)
                .ToListAsync();
        }

        public async Task UpdateAsync(ClassGroupSubject classGroupSubject)
        {
            _dbContext.ClassGroupSubject.Update(classGroupSubject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var classGroupSubject = await _dbContext.ClassGroupSubject.FindAsync(id);
            if (classGroupSubject != null)
            {
                _dbContext.ClassGroupSubject.Remove(classGroupSubject);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

