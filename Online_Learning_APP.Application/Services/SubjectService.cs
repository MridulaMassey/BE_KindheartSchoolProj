using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_App.Infrastructure.Repository;
using System.Diagnostics;
using AutoMapper;

namespace Online_Learning_APP.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }


        public async Task<Guid> CreateSubjectAsync(SubjectDto subjectDto)
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            subject.SubjectId = Guid.NewGuid(); // Generate a new ID



            await _subjectRepository.AddAsync(subject);
            // return _mapper.Map<SubjectDto>(subject);
            return subject.SubjectId;
        }
        public async Task<SubjectDto> GetSubjectByIdAsync(Guid subjectId)
        {
            var subject = await _subjectRepository.GetSubjectByIdAsync(subjectId);
            if (subject == null)
                return null;
            return subject == null ? null : _mapper.Map<SubjectDto>(subject);
          
        }
        public async Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync()
        {
            var subject = await _subjectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubjectDto>>(subject);
        }

        public async Task<SubjectDto> UpdateSubjectAsync(Guid activityId, UpdateSubjectDto updateSubjectDto)
        {
            var subject = await _subjectRepository.GetSubjectByIdAsync(activityId);
            if (subject == null)
            {
                return null;
            }

            // Update properties
            subject.SubjectName = updateSubjectDto.SubjectName ?? subject.SubjectName;
            //activity.Description = updateSubjectDto.Description ?? activity.Description;

            await _subjectRepository.UpdateAsync(subject);
            return _mapper.Map<SubjectDto>(subject);
        }

        public async Task<bool> DeleteSubjectAsync(Guid subjectId)
        {
            var subject = await _subjectRepository.GetSubjectByIdAsync(subjectId);
            if (subject == null)
            {
                return false;
            }

       
            await _subjectRepository.DeleteAsync(subjectId);
            return true;
        }
    }
}
