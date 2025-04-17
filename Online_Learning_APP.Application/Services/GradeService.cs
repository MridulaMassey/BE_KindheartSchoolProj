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

namespace Online_Learning_APP.Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext _context;

        public GradeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AssignGradeToActivity(ActivityGradeDto activityGradeDto)
        {
            var activityGrade = new ActivityGrade
            {
                ActivityGradeId = Guid.NewGuid(),
                StudentId = activityGradeDto.StudentId,
                ActivityId = activityGradeDto.ActivityId,
                Score = activityGradeDto.Score
            };

            await _context.ActivityGrade.AddAsync(activityGrade);
            await _context.SaveChangesAsync();
        }
        public async Task AssignGradeToActivityTeacher(ActivityGradeDto activityGradeDto)
        {
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
            var activities = _context.Activities
                .Where(a => a.SubjectId == finalGradeDto.SubjectId)
                .ToList();

            var studentGrades = _context.ActivityGrade
                .Where(ag => ag.StudentId == finalGradeDto.StudentId && activities.Select(a => a.ActivityId).Contains(ag.ActivityId))
                .ToList();

            if (!activities.Any() || !studentGrades.Any()) return 0;

            double finalScore = studentGrades.Sum(ag =>
            {
                var activity = activities.First(a => a.ActivityId == ag.ActivityId);
                return (ag.Score * activity.WeightagePercent) / 100;
            });

            return finalScore;
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
    }
}
