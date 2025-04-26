using System;
using System.Threading.Tasks;
using AutoMapper; // Assuming you're using AutoMapper
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

//using Online_Learning_App.Application.DTOs;
//using Online_Learning_App.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_App.Infrastructure;
using Online_Learning_App.Infrastructure.Migrations;
using Online_Learning_App.Infrastructure.Repository;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Handler;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_APP.Application.Services;

//using Online_Learning_App.Infrastructure.Persistence.Interfaces; // Assuming a repository

namespace Online_Learning_App.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private IFileUploadService _uploadService;
        private readonly IClassGroupSubjectRepository _classGroupSubjectRepository;
        private readonly IClassGroupSubjectActivityRepository _classGroupSubjectActivityRepository;
        private readonly IClassGroupSubjectStudentActivityRepository _classgroupsubjectstudentActivityrepository;
        private readonly IGradeService _gradeService;
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;
        public ActivityService(IActivityRepository activityRepository, IMapper mapper, IFileUploadService uploadService, IClassGroupSubjectRepository classGroupSubjectRepository, IClassGroupSubjectActivityRepository classGroupSubjectActivityRepository, IGradeService gradeService,
            ApplicationDbContext dbContext, IClassGroupSubjectStudentActivityRepository classgroupsubjectstudentActivityrepository
            , INotificationService notificationService, IMediator mediator)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _uploadService = uploadService;
            _classGroupSubjectRepository = classGroupSubjectRepository;
            _classGroupSubjectActivityRepository = classGroupSubjectActivityRepository;
            _gradeService = gradeService;
            _classgroupsubjectstudentActivityrepository = classgroupsubjectstudentActivityrepository;
            _dbContext = dbContext;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task<ActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto)
        {
            byte[] filebytetest = Convert.FromBase64String(createActivityDto.PdfFileBase64);
            var response = await _uploadService.UploadFileAsync(filebytetest, createActivityDto.FileName);
            //    var subjectidrespnse= await  _classGroupSubjectRepository.GetByClassGroupIdAsync(createActivityDto.ClassGroupId.Value);
            // var subjectID= subjectidrespnse.FirstOrDefault().SubjectId;
            //   var classGroupid= subjectidrespnse.FirstOrDefault()?.ClassGroupId;

            // Fetch existing activities for the subject and class
            var existingActivities = await _activityRepository.GetBySubjectAndClassAsync(createActivityDto.SubjectId, createActivityDto.ClassGroupId.Value);

            // Calculate total weightage including the new activity
            //   double totalWeightage = existingActivities.Sum(a => a.WeightagePercent) + createActivityDto.WeightagePercent;
            double totalWeightage = createActivityDto.WeightagePercent;
            if (totalWeightage > 100)
            {
                throw new InvalidOperationException("Total weightage percent cannot exceed 100 percent.");
            }
            var classgroupsubjectid = Guid.NewGuid();
            var classGroupSubject = new ClassGroupSubject
            {
                ClassGroupSubjectId = classgroupsubjectid,
                ClassGroupId = createActivityDto.ClassGroupId.Value,
                SubjectId = createActivityDto.SubjectId
            };

            await _classGroupSubjectRepository.AddAsync(classGroupSubject);

            var activity = _mapper.Map<Activity>(createActivityDto);
            activity.Feedback = createActivityDto.Feedback;
            activity.HasFeedback = createActivityDto?.HasFeedback.Value;
            activity.ActivityId = Guid.NewGuid(); // Generate a new ID
            activity.Id = activity.ActivityId; // If Id is needed.
            activity.SubjectId = createActivityDto.SubjectId;
            activity.ClassGroupId = createActivityDto.ClassGroupId;
            activity.ClassLevel = "Four";
            activity.PdfUrl = response.ToString();
            activity.WeightagePercent = createActivityDto.WeightagePercent;
            activity.ClassGroupSubjectId = classgroupsubjectid;
            var classgrpactivity = Guid.NewGuid();
            //await _classGroupSubjectRepository.AddAsync(classGroupSubject);
            activity.TeacherId = createActivityDto.TeacherId;



            var classGroupSubjectActivity = new ClassGroupSubjectActivity
            {
                ClassGroupSubjectActivityId = classgrpactivity,
                ClassGroupSubjectId = classgroupsubjectid,
                ActivityId = activity.ActivityId,
            };
            //   var classGroups = _dbContext.Students.Where(a=>a.ClassGroupId== createActivityDto.ClassGroupId);
            var allClassGroupIds = await _dbContext.Students.Where(a => a.ClassGroupId == createActivityDto.ClassGroupId)
              .Select(s => s.ClassGroupId)
            // .Distinct()
            .ToListAsync();

            // await _classGroupSubjectActivityRepository.CreateAsync(classGroupSubjectActivity);
            await _activityRepository.AddAsync(activity);
            await _classGroupSubjectActivityRepository.CreateAsync(classGroupSubjectActivity);


            List<Guid?> targetClassGroupIds = new List<Guid?> { createActivityDto.ClassGroupId };
            if (createActivityDto.ClassGroupId == null)
            {
                // Handle the case where ClassGroupId is not provided in DTO if needed
                // For example, you might want to throw an error or process all groups.
                // For this example, let's assume you process all if null.
                targetClassGroupIds = allClassGroupIds;
            }

            foreach (var classGroupId in targetClassGroupIds.Where(id => id.HasValue).Select(id => id.Value).Distinct())
            {
                var studentsInClassGroup = await _dbContext.Students
                    .Where(s => s.ClassGroupId == classGroupId)
                    .ToListAsync();

                foreach (var student in studentsInClassGroup)
                {
                    var classgrpactivityc = Guid.NewGuid(); // Generate a unique ID for each record

                    var classGroupSubjectStudentActivityC = new ClassGroupSubjectStudentActivity
                    {
                        ClassGroupSubjectStudentActivityId = classgrpactivityc,
                        ClassGroupSubjectId = classgroupsubjectid,
                        ActivityId = activity.ActivityId, // Assuming 'activity' is defined elsewhere and has an ActivityId
                        StudentId = student.Id // Use the actual StudentId from the student object
                      //  fee
                    };

                  //  await _classgroupsubjectstudentActivityrepository.AddAsync(classGroupSubjectStudentActivityC);
                    await _mediator.Send(new CreateActivityCommand
                    {
                        ClassGroupSubjectStudentActivityId = classgrpactivityc,
                        ClassGroupSubjectId = classgroupsubjectid,
                        ActivityId = activity.ActivityId, // Assuming 'activity' is defined elsewhere and has an ActivityId
                        StudentId = student.Id
                    });

                   // await _notificationService.NotifyStudentAsync(student.Id, $"New activity posted: {activity.Title}");
                }


                //var classGroupSubjectStudentActivity = new ClassGroupSubjectStudentActivity
                //{
                //    ClassGroupSubjectStudentActivityId = classgrpactivity,
                //    ClassGroupSubjectId = classgroupsubjectid,
                //    ActivityId = activity.ActivityId,
                //    StudentId = new Guid("845DB027-2D1D-46D5-5634-08DD65188216")
                //    // StudentId =activity.StudentId.Value
                //};
                //await _classgroupsubjectstudentActivityrepository.AddAsync(classGroupSubjectStudentActivity);

            }
         //   var updatedActivities = await _mediator.Send(new GetAllActivitiesQuery());

        
         //   await _hubContext.Clients.All.SendAsync("ReceiveActivitiesList", updatedActivities);
            return _mapper.Map<ActivityDto>(activity);
        }



        public async Task<ActivityDto> UpdateTeacherActivityAsync(UpdateTeacherSubmissionDto createActivityDto)
        {

            var classgroupsubjectid = Guid.NewGuid();

            var activity = await _activityRepository.GetByIdAsync(createActivityDto.ActivityId);
        //    var studentGuid = new Guid("845DB027-2D1D-46D5-5634-08DD65188216"); //update techer ID
            activity.Feedback = createActivityDto.Feedback;  //added merl
            activity.HasFeedback = true; //added merl


            var activityGrade = new ActivityGradeDto
            {
                //ActivityGradeId = Guid.NewGuid(),
                StudentId = createActivityDto.StudentId,
                ActivityId = createActivityDto.ActivityId,
                Score = createActivityDto.Grade.Value,
            };
            var finalGrade = new FinalGradeDto
            {
                //ActivityGradeId = Guid.NewGuid(),
                StudentId = createActivityDto.StudentId,
                SubjectId = activity.SubjectId
            };
            //  var activitygradeId=_gradeService.get
            await _gradeService.AssignGradeToActivityTeacher(activityGrade);
            //var activityGradeObject= new ActivityGradeDto
            //{

            //    StudentId = activityGrade.StudentId,
            //    ActivityId = createActivityDto.ActivityId,
            //    Score = createActivityDto.Grade.Value
            //};

           var activityFeedback = await _gradeService.CalculateFinalGrade(finalGrade);
            var existingSubmission = await _dbContext.Submissions
       .FirstOrDefaultAsync(s => s.StudentId == createActivityDto.StudentId && s.ActivityId == createActivityDto.ActivityId);

            if (existingSubmission != null)
            {
                //If submission exists, update it instead of creating a new one
                //existingSubmission.PdfUrl = createActivityDto.pd ?? "";
                existingSubmission.SubmissionDate = DateTime.UtcNow;
                //   existingSubmission.StudentComment = dto.StudentComment ?? "";
                existingSubmission.Grade = createActivityDto.Grade.Value;
                existingSubmission.Feedback= createActivityDto.Feedback;
                _dbContext.Submissions.Update(existingSubmission);


            }

            var classGroupSubjectStudentActivityC = await _classgroupsubjectstudentActivityrepository.GetActivitySubjectStudentByIdAsync(createActivityDto.ActivityId, createActivityDto.StudentId);
            // classGroupSubjectStudentActivityC.
           // classGroupSubjectStudentActivityC.SubmissionId = su;
            classGroupSubjectStudentActivityC.Feedback = createActivityDto.Feedback;
            // classGroupSubjectStudentActivityC.Gr = createActivityDto.Feedback;
            await _classgroupsubjectstudentActivityrepository.UpdateAsync(classGroupSubjectStudentActivityC);
            //var submission = new Submission
            //{
            //    SubmissionId = Guid.NewGuid(),
            //    ActivityId = createActivityDto.ActivityId,
            //    StudentId = createActivityDto.StudentId,
            //   // PdfUrl = createActivityDto.p,
            //    SubmissionDate = DateTime.UtcNow,
            //    Feedback = "",
            //    Grade = createActivityDto.Grade.Value,
            //  //  StudentComment = dto.StudentComment
            //};


            //_context.Submissions.Update(submission);
          //  _context.Submissions.Add(submission);
            //  _classgroupsubjectstudentActivityrepository.UpdateAsync()
            //await _gradeService.AssignGradeToActivity(activityGradeObject);
            await _activityRepository.UpdateAsync(activity);
            //   await _classGroupSubjectActivityRepository.CreateAsync(classGroupSubjectActivity);
            return _mapper.Map<ActivityDto>(activity);
        }
        public async Task<ActivityDto> GetActivityByIdAsync(Guid activityId)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            return activity == null ? null : _mapper.Map<ActivityDto>(activity);
        }

        // Read All Activities
        public async Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync()
        {
            var activities = await _activityRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        //  Update Activity by teacher
        public async Task<ActivityDto> UpdateActivityAsync(Guid activityId, UpdateActivityDto updateActivityDto)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            if (activity == null)
            {
                return null;
            }

          
            //activity.ActivityName = updateActivityDto.ActivityName ?? activity.ActivityName;
            //activity.Description = updateActivityDto.Description ?? activity.Description;


            // byte array
            byte[] filebytetest = Convert.FromBase64String(updateActivityDto.FileBase64);
            var response = await _uploadService.UploadFileAsync(filebytetest, updateActivityDto.FileName);
            activity.StudentPdfUrl = response.ToString();
            //     activity.StudentPdfUrl = updateActivityDto.FileBase64 ?? activity.Description;


            await _activityRepository.UpdateAsync(activity);
            return _mapper.Map<ActivityDto>(activity);
        }

        // Delete Activity
        public async Task<bool> DeleteActivityAsync(Guid activityId)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            if (activity == null)
            {
                return false;
            }

            await _activityRepository.DeleteAsync(activity.Id);
            return true;
        }
        //merl: added for upcoming activities on student dashboard/activities
        public async Task<List<Activity>> GetUpcomingActivitiesForStudent(Guid studentId)
        {
            var today = DateTime.UtcNow;
            ///  var test = await _dbContext.Students.ToListAsync();

            var classGroupId = await _dbContext.Students
                .Where(s => s.Id == studentId)
                .Select(s => s.ClassGroupId)
            .FirstOrDefaultAsync();

            var upcoming = await _dbContext.Activities
                .Where(a => a.ClassGroupId == classGroupId && a.DueDate > today)
                .OrderBy(a => a.DueDate)
                .ToListAsync();

            return upcoming;
        }

    }
}