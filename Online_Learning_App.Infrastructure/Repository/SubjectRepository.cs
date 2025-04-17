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
    public class SubjectRepository: ISubjectRepository
    {
        private readonly ApplicationDbContext _dbContext; // Replace with your DbContext
        public SubjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Subject subject)
        {
            await _dbContext.Subjects.AddAsync(subject);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Subject> GetSubjectByIdAsync(Guid id)
        {
            return await _dbContext.Subjects.FindAsync(id);
        }
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _dbContext.Subjects.ToListAsync();
        }
        public async Task UpdateAsync(Subject subject)
        {
            _dbContext.Subjects.Update(subject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var subject = await _dbContext.Subjects.FindAsync(id);
            if (subject != null)
            {
                _dbContext.Subjects.Remove(subject);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
