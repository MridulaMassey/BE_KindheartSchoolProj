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
        private readonly IActivityRepository _activityRepository;
        //private readonly IMapper _mapper;
        private IFileUploadService _uploadService;

        public ClassGroupSubjectStudentActivityService(IClassGroupSubjectStudentActivityRepository repository, IMapper mapper, IActivityRepository activityRepository,
            IFileUploadService uploadService)
        {
            _repository = repository;
            _mapper = mapper;
            _activityRepository = activityRepository;
            _uploadService = uploadService;
        }
        public async Task<Guid> CreateSubjectAsync(ClassGroupSubjectStudentActivityDto classgroupsubjectStudentActivityDto)
        {
            var subject = _mapper.Map<ClassGroupSubjectStudentActivity>(classgroupsubjectStudentActivityDto);
            subject.ClassGroupSubjectStudentActivityId = Guid.NewGuid(); // Generate a new ID



            await _repository.AddAsync(subject);
            // return _mapper.Map<SubjectDto>(subject);
            return subject.ClassGroupSubjectStudentActivityId;
        }

        public async Task<IEnumerable<ClassGroupSubjectStudentActivityDto>> GetClassGroupByStudentByIdAsync(GetClassGroupSubStudActivityDto clg)
        {
            var subject = await _repository.GetClassGroupSubjectActivityStudentByIdAsync(clg.ActivityId,clg.StudentId);
            if (subject == null)
                return null;
            return subject == null ? null : _mapper.Map<IEnumerable<ClassGroupSubjectStudentActivityDto>>(subject);

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

        public async Task<ClassGroupSubjectStudentActivityDto> UpdateSubjectAsync(UpdateClassGroupSubjectStudentActivityDto updateSubjectDto)
        {
            var subject = await _repository.GetClassGroupSubjectActivityStudentByIdAsync(updateSubjectDto.ActivityId, updateSubjectDto.StudentId);
            if (subject == null)
            {
                return null;
            }

            var activity = await _activityRepository.GetByIdAsync(updateSubjectDto.ActivityId);
            if (activity == null)
            {
                return null;
            }


           
            // byte array
            //byte[] filebytetest = Convert.FromBase64String(updateSubjectDto.FileBase64);
            //var response = await _uploadService.UploadFileAsync(filebytetest, updateSubjectDto.FileName);
            var classGroupStudentSubjectRepository = new ClassGroupSubjectStudentActivity
            {  ActivityId= updateSubjectDto.ActivityId,
                pdfUrl = updateSubjectDto.FileBase64,
                ClassGroupSubjectId=activity.ClassGroupSubjectId.Value,
                StudentId= updateSubjectDto.StudentId

            };

           await _repository.UpdateAsync(classGroupStudentSubjectRepository);
            // Update properties
            //  subject = updateSubjectDto.ActivityId;


            //  await _repository.UpdateAsync(subject);
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
