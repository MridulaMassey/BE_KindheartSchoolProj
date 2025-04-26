using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Online_Learning_APP.Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GradeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AssignGradeToActivity(ActivityGradeDto activityGradeDto)
        {
            var clgactivity = await _context.ClassGroupSubjectStudentActivity.Where(a => a.ActivityId == activityGradeDto.ActivityId && a.StudentId == activityGradeDto.StudentId).Include(a=>a.Submission).FirstOrDefaultAsync();
            var score = clgactivity?.Submission?.Grade;
            var activityGrade = new ActivityGrade
            {
                ActivityGradeId = Guid.NewGuid(),
                StudentId = activityGradeDto.StudentId,
                ActivityId = activityGradeDto.ActivityId,
               Score = score.Value
            };
            if(score>0)
            { 
            await _context.ActivityGrade.AddAsync(activityGrade);
            await _context.SaveChangesAsync();
            }
        }
        public async Task AssignGradeToActivityTeacher(ActivityGradeDto activityGradeDto)
        {
         //   var score = _context.ClassGroupSubjectStudentActivity.FirstOrDefault(a => a.ActivityId == activityGradeDto.ActivityId && a.StudentId== activityGradeDto.StudentId)?.Submission?.Grade;

            var activityGrade = new ActivityGrade
            {
                ActivityGradeId = Guid.NewGuid(),
                StudentId = activityGradeDto.StudentId,
                ActivityId = activityGradeDto.ActivityId,
                Score = activityGradeDto.Score
            };
    

          var activityId = _context.ActivityGrade.FirstOrDefault(a => a.ActivityId == activityGrade.ActivityId);
            if (activityId == null)
            {
              
                await _context.ActivityGrade.AddAsync(activityGrade);
            }
           
            await _context.SaveChangesAsync();
        }

        public async Task<double> CalculateFinalGrade(FinalGradeDto finalGradeDto)
        {
            // var stdActivityStudent=_context.ClassGroupSubjectStudentActivity.Select

            var activities = _context.Activities
                .Where(a => a.SubjectId == finalGradeDto.SubjectId)
                .ToList();

            var studentGrades = _context.ActivityGrade
                .Where(ag => ag.StudentId == finalGradeDto.StudentId && activities.Select(a => a.ActivityId).Contains(ag.ActivityId.Value))
                .ToList();

            if (!activities.Any() || !studentGrades.Any()) return 0;

            double finalScore = studentGrades.Sum(ag =>
            {
                var activity = activities.First(a => a.ActivityId == ag.ActivityId);
                return (ag.Score * activity.WeightagePercent) / 100;
            });

            return finalScore;
        }

        public async Task<double> CalculateFinalGradeForActivity(FinalGradeDto finalGradeDto)
        {
            // var stdActivityStudent=_context.ClassGroupSubjectStudentActivity.Select
            //var tactivities = _context.Activities
            //    .Where(a => a.SubjectId == finalGradeDto.SubjectId)
            //    .ToList();
            //var clgsubjectID = _context.ClassGroupSubjectActivities
            //    .Where(a => a.ActivityId == finalGradeDto.ActivityId)
            //    .ToList();

            var activities = _context.ClassGroupSubjectStudentActivity
                .Where(a => a.ClassGroupSubjectId == finalGradeDto.clgSubjectId)
                .ToList();

            var studentGrades = _context.ActivityGrade
                .Where(ag => ag.StudentId == finalGradeDto.StudentId && activities.Select(a => a.ActivityId).Contains(ag.ActivityId.Value))
                .ToList();

            if (!activities.Any() || !studentGrades.Any()) return 0;

            double finalScore = studentGrades.Sum(ag =>
            {
                var activity = _context.Activities.First(a => a.ActivityId == ag.ActivityId);
                return (ag.Score * activity.WeightagePercent) / 100;
            });

            return finalScore;
        }
        public async Task<ActivityGradeDto> GetFinalGradebyStdID(Guid studentID)
        {


            var activityGrade= await _context.ActivityGrade.FirstOrDefaultAsync(a=> a.StudentId== studentID);
            var activity = _mapper.Map<ActivityGradeDto>(activityGrade);
            await _context.SaveChangesAsync();

            return activity;
        }

        public async Task<bool> ReleaseFinalGrade(FinalGradeDto finalGradeDto)
        {
            double finalScore = await CalculateFinalGrade(finalGradeDto);
            string grade = finalScore >= 90 ? "A" : finalScore >= 80 ? "B" : finalScore >= 70 ? "C" : "D";

            var finalGrade = new FinalGrade
            {
                FinalGradeId = Guid.NewGuid(),
                StudentId = finalGradeDto.StudentId,
                SubjectId = finalGradeDto.SubjectId,
                FinalScore = finalScore,
                Grade = grade,
                IsReleased = true
            };

            await _context.FinalGrade.AddAsync(finalGrade);
            await _context.SaveChangesAsync();

            return true;
        }
        //public async Task<Dictionary<Guid, double>> CalculateFinalGradesForActivityold(FinalGradeBatchDto finalGradeDto)
        //{
        //    // Get all student-activity mappings for the given classGroupSubject
        //    var studentActivities = await _context.ClassGroupSubjectStudentActivity
        //        .Where(a => a.ClassGroupSubjectId == finalGradeDto.clgSubjectId)
        //        .ToListAsync();

        //    // Group activities by student
        //    var groupedByStudent = studentActivities
        //        .GroupBy(sa => sa.StudentId);

        //    var finalGrades = new Dictionary<Guid, double>();

        //    foreach (var studentGroup in groupedByStudent)
        //    {
        //        var studentId = studentGroup.Key;

        //        double finalScore = 0;

        //        foreach (var sa in studentGroup)
        //        {
        //            var clggrpactivity=  _context.ClassGroupSubjectStudentActivity.Include(a=>a.Submission).FirstOrDefault(a => a.ActivityId == sa.ActivityId && a.StudentId== studentId);
        //            var score = clggrpactivity?.Submission?.Grade==null?0: clggrpactivity?.Submission?.Grade;
        //            if (score.Value==0 ||score < 1)
        //            { continue; }
        //            // Get activity
        //            var activity = await _context.Activities.FirstOrDefaultAsync(a => a.ActivityId == sa.ActivityId);
        //            if (activity == null) continue;

        //            // Get submission
        //            var submission = await _context.Submissions
        //                .FirstOrDefaultAsync(s => s.StudentId == studentId && s.ActivityId == sa.ActivityId);

        //            if (submission == null) continue;

        //            // Calculate weighted score
        //            finalScore += (submission.Grade * activity.WeightagePercent) / 100.0;
        //        }

        //        finalGrades[studentId] = finalScore;
        //    }

        //    return finalGrades;
        //}


        ///
        //public async Task<Dictionary<Guid, double>> CalculateFinalGradesForActivity(FinalGradeBatchDto finalGradeDto)
        //{
        //    // Load all relevant student-activity mappings for the given classGroupSubject
        //    var studentActivities = await _context.ClassGroupSubjectStudentActivity
        //        .Where(a => a.ClassGroupSubjectId == finalGradeDto.clgSubjectId)
        //        .Include(a => a.Submission) // Include related submission
        //        .ToListAsync();

        //    // Get all relevant activity definitions to use their WeightagePercent
        //    var activityIds = studentActivities.Select(sa => sa.ActivityId).Distinct();
        //    var activities = await _context.Activities
        //        .Where(a => activityIds.Contains(a.ActivityId))
        //        .ToDictionaryAsync(a => a.ActivityId);

        //    // Group studentActivities by student
        //    var groupedByStudent = studentActivities
        //        .Where(sa => sa.Submission != null &&  sa.Submission.Grade > 0)
        //        .GroupBy(sa => sa.StudentId);

        //    var finalGrades = new Dictionary<Guid, double>();

        //    foreach (var studentGroup in groupedByStudent)
        //    {
        //        var studentId = studentGroup.Key;
        //        double finalScore = 0;

        //        foreach (var sa in studentGroup)
        //        {
        //            if (activities.TryGetValue(sa.ActivityId, out var activity))
        //            {
        //                var grade = sa.Submission.Grade;
        //                finalScore += (grade * activity.WeightagePercent) / 100.0;
        //            }
        //        }

        //        finalGrades[studentId] = finalScore;
        //    }

        //    return finalGrades;
        //}

        public async Task<Dictionary<Guid, double>> CalculateFinalGradesForActivity(List<Guid> studentIds)
        {
            // Get all student-activity mappings for the given students (across all classGroupSubjects)
            var studentActivities = await _context.ClassGroupSubjectStudentActivity
                .Where(a => studentIds.Contains(a.StudentId))
                .Include(a => a.Submission)
                .ToListAsync();

            // Get distinct activity IDs
            var activityIds = studentActivities.Select(sa => sa.ActivityId).Distinct();

            // Fetch all related activities and their weightages
            var activities = await _context.Activities
                .Where(a => activityIds.Contains(a.ActivityId))
                .ToDictionaryAsync(a => a.ActivityId);

            // Group by student ID
            var groupedByStudent = studentActivities
                .Where(sa => sa.Submission != null && sa.Submission.Grade!=0 && sa.Submission.Grade > 0)
                .GroupBy(sa => sa.StudentId);

            var finalGrades = new Dictionary<Guid, double>();

            foreach (var studentGroup in groupedByStudent)
            {
                var studentId = studentGroup.Key;
                double finalScore = 0;
                double totalWeightage = 0; // 🔧 added

                foreach (var sa in studentGroup)
                {
                    if (activities.TryGetValue(sa.ActivityId, out var activity))
                    {
                        finalScore += (sa.Submission.Grade * activity.WeightagePercent) / 100.0;
                        totalWeightage += activity.WeightagePercent; // 🔧 added
                    }
                }
                finalGrades[studentId] = totalWeightage > 0 ? (finalScore / totalWeightage) * 100.0 : 0;
                // finalGrades[studentId] = finalScore;
            }

            return finalGrades;
        }


    }
}
