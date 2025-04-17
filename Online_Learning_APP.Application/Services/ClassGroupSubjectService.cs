using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_APP.Application.DTO;
using Online_Learning_App.Infrastructure.Repository;
using AutoMapper;

namespace Online_Learning_APP.Application.Services
{
    public class ClassGroupSubjectService : IClassGroupSubjectService
    {
        private readonly IClassGroupSubjectRepository _classGroupSubjectRepository;
        private readonly IMapper _mapper;

        public ClassGroupSubjectService(IClassGroupSubjectRepository classGroupSubjectRepository, IMapper mapper)
        {
            _classGroupSubjectRepository = classGroupSubjectRepository;
            _mapper = mapper;
        }

        public async Task<ClassGroupSubjectDto> AddClassGroupSubjectAsync(ClassGroupSubjectDto classGroupSubject)
        {
            var activity = _mapper.Map<ClassGroupSubject>(classGroupSubject);
            activity.ClassGroupSubjectId = Guid.NewGuid(); // Generate a new ID
            activity.SubjectId = classGroupSubject.SubjectId; // If Id is needed.
            activity.ClassGroupId = classGroupSubject.ClassGroupId;
            await _classGroupSubjectRepository.AddAsync(activity);
            //  await _classGroupSubjectRepository.AddAsync(classGroupSubject);
            return _mapper.Map<ClassGroupSubjectDto>(activity);
        }

        public async Task<ClassGroupSubjectDto> GetClassGroupSubjectByIdAsync(Guid id)
        {
            var subjectGroup = await _classGroupSubjectRepository.GetByIdAsync(id);
            return subjectGroup == null ? null : _mapper.Map<ClassGroupSubjectDto>(subjectGroup);
            //  return await _classGroupSubjectRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ClassGroupSubjectDto>> GetAllClassGroupSubjectsAsync()
        {
            var response = await _classGroupSubjectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClassGroupSubjectDto>>(response);

        }

        public async Task<IEnumerable<ClassGroupSubject>> GetByClassGroupIdAsync(Guid classGroupId)
        {
            return await _classGroupSubjectRepository.GetByClassGroupIdAsync(classGroupId);
        }


        public async Task<IEnumerable<ClassGroupSubjectDto>> GetBySubjectIdAsync(Guid subjectId)
        {
            var subjectGroup = await _classGroupSubjectRepository.GetBySubjectIdAsync(subjectId);
            return subjectGroup == null ? null : _mapper.Map<IEnumerable<ClassGroupSubjectDto>>(subjectGroup);

            // return await _classGroupSubjectRepository.GetBySubjectIdAsync(subjectId);
        }

        public async Task UpdateClassGroupSubjectAsync(ClassGroupSubjectDto classGroupSubject)
        {

            var activity = await _classGroupSubjectRepository.GetByIdAsync(classGroupSubject.ClassGroupSubjectId.Value);
            //if (activity == null)
            //{
            //    return null;
            //}

            // Update properties
            activity.SubjectId = classGroupSubject.SubjectId;
            activity.ClassGroupId = classGroupSubject.ClassGroupId;
            await _classGroupSubjectRepository.UpdateAsync(activity);
        }

        public async Task DeleteClassGroupSubjectAsync(Guid id)
        {
            await _classGroupSubjectRepository.DeleteAsync(id);
        }
    }
}
