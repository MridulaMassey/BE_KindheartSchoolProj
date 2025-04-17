using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_APP.Application.DTO;
using Online_Learning_App.Infrastructure.Repository;
using AutoMapper;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_APP.Application.Services
{
    public class ClassGroupSubjectStudentActivityService: IClassGroupSubjectStudentActivityService
    {
        private readonly IClassGroupSubjectStudentActivityRepository _repository;
        private readonly IMapper _mapper;

        public ClassGroupSubjectStudentActivityService(IClassGroupSubjectStudentActivityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Guid> CreateSubjectAsync(ClassGroupSubjectStudentActivityDto classgroupsubjectStudentActivityDto)
        {
            var subject = _mapper.Map<ClassGroupSubjectStudentActivity>(classgroupsubjectStudentActivityDto);
            subject.ClassGroupSubjectStudentActivityId = Guid.NewGuid(); // Generate a new ID



            await _repository.AddAsync(subject);
            // return _mapper.Map<SubjectDto>(subject);
            return subject.ClassGroupSubjectStudentActivityId;
        }
        public async Task<IEnumerable<ClassGroupSubjectStudentActivityDto>> GetSubjectByIdAsync(Guid subjectId)
        {
            var subject = await _repository.GetClassGroupSubjectActivityByIdAsync(subjectId);
            if (subject == null)
                return null;
            return subject == null ? null : _mapper.Map<IEnumerable<ClassGroupSubjectStudentActivityDto>>(subject); 

        }
        public async Task<IEnumerable<ClassGroupSubjectStudentActivityDto>> GetAllSubjectsAsync()
        {
            var subject = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClassGroupSubjectStudentActivityDto>>(subject);
        }

        public async Task<ClassGroupSubjectStudentActivityDto> UpdateSubjectAsync(Guid activityId, ClassGroupSubjectStudentActivityDto updateSubjectDto)
        {
            var subject = await _repository.GetClassGroupSubjectActivityByIdAsync(activityId);
            if (subject == null)
            {
                return null;
            }

            // Update properties
         //   subject.ClassGroupSubjectStudentActivityId = updateSubjectDto.ActivityId;
            //activity.Description = updateSubjectDto.Description ?? activity.Description;

       //     await _repository.UpdateAsync(subject);
            return _mapper.Map<ClassGroupSubjectStudentActivityDto>(subject);
        }

        public async Task<bool> DeleteSubjectAsync(Guid subjectId)
        {
            var subject = await _repository.GetClassGroupSubjectActivityByIdAsync(subjectId);
            if (subject == null)
            {
                return false;
            }


            await _repository.DeleteAsync(subjectId);
            return true;
        }

    }
}
