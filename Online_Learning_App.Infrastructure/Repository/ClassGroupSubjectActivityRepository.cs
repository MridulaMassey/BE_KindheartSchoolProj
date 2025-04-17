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
    public class ClassGroupSubjectActivityRepository : IClassGroupSubjectActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassGroupSubjectActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       //public async Task<IEnumerable<ClassGroupSubjectActivity>> GetAllAsync() => await _context.ClassGroupSubjectActivities.ToListAsync();
        public async Task<IEnumerable<ClassGroupSubjectActivity>> GetAllAsync()
        {
            return await _context.ClassGroupSubjectActivities.Include(a=>a.ClassGroupSubject).ThenInclude(a=>a.ClassGroup).ThenInclude(a=>a.Students).Include(a=>a.Activity).ToListAsync();
        }

        public async Task<ClassGroupSubjectActivity> GetByIdAsync(Guid id) => await _context.ClassGroupSubjectActivities.FindAsync(id);
        public async Task<ClassGroupSubjectActivity> CreateAsync(ClassGroupSubjectActivity entity)
        {
          //  _context.ClassGroupSubjectActivities.Add(entity);
          //return  await _context.SaveChangesAsync();
            await _context.ClassGroupSubjectActivities.AddAsync(entity);
             await _context.SaveChangesAsync();
             return entity;
        }
        public async Task<ClassGroupSubjectActivity> UpdateAsync(ClassGroupSubjectActivity entity)
        {
            _context.ClassGroupSubjectActivities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.ClassGroupSubjectActivities.FindAsync(id);
            if (entity == null) return false;
            _context.ClassGroupSubjectActivities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
