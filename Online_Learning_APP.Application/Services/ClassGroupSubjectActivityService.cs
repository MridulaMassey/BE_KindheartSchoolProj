using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;

namespace Online_Learning_APP.Application.Services
{
    public class ClassGroupSubjectActivityService : IClassGroupSubjectActivityService
    {
        private readonly IClassGroupSubjectActivityRepository _repository;

        public ClassGroupSubjectActivityService(IClassGroupSubjectActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClassGroupSubjectActivity>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<ClassGroupSubjectActivity> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
        public async Task<ClassGroupSubjectActivity> CreateAsync(ClassGroupSubjectActivity entity) => await _repository.CreateAsync(entity);
        public async Task<ClassGroupSubjectActivity> UpdateAsync(ClassGroupSubjectActivity entity) => await _repository.UpdateAsync(entity);
        public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}
