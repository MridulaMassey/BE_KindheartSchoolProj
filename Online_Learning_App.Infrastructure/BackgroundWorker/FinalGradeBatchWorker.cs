using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;

namespace Online_Learning_App.Infrastructure.BackgroundWorker
{
    public class FinalGradeBatchWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FinalGradeBatchWorker> _logger;

        public FinalGradeBatchWorker(IServiceProvider serviceProvider, ILogger<FinalGradeBatchWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("FinalGradeBatchWorker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var studentActivityRepository = scope.ServiceProvider.GetRequiredService<IClassGroupSubjectStudentActivityRepository>();
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); // 👈 Inject DbContext

                        var students = await studentActivityRepository.GetAllAsync();
                        var studentIds = students.Select(s => s.StudentId).Distinct().ToList();

                        var finalGrades = await CalculateFinalGradesForActivity(studentIds, dbContext, stoppingToken);

                        // You can log or send the finalGrades somewhere if needed
                        _logger.LogInformation($"Calculated final grades for {finalGrades.Count} students.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred in FinalGradeBatchWorker.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("FinalGradeBatchWorker stopped.");
        }
        private async Task<Dictionary<Guid, double>> CalculateFinalGradesForActivity(List<Guid> studentIds, ApplicationDbContext _context, CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            { 
                var studentActivities = await _context.ClassGroupSubjectStudentActivity
                .Where(a => studentIds.Contains(a.StudentId))
                .Include(a => a.Submission)
                .ToListAsync(cancellationToken);

            var activityIds = studentActivities.Select(sa => sa.ActivityId).Distinct();

            var activities = await _context.Activities
                .Where(a => activityIds.Contains(a.ActivityId))
                .ToDictionaryAsync(a => a.ActivityId, cancellationToken);

            var groupedByStudent = studentActivities
                .Where(sa => sa.Submission != null && sa.Submission.Grade != 0 && sa.Submission.Grade > 0)
                .GroupBy(sa => sa.StudentId);
               // var groupedByStudent= groupedByStudent.A
            var finalGrades = new Dictionary<Guid, double>();

            foreach (var studentGroup in groupedByStudent)
            {
                  //  studentGroup.FirstOrDefault().Ac
                var studentId = studentGroup.Key;
                double finalScore = 0;
                double totalWeightage = 0;

                foreach (var sa in studentGroup)
                {
                    if (activities.TryGetValue(sa.ActivityId, out var activity))
                    {
                        finalScore += (sa.Submission.Grade * activity.WeightagePercent) / 100.0;
                        totalWeightage += activity.WeightagePercent;
                    }

                    }
                    finalGrades[studentId] = totalWeightage > 0 ? (finalScore / totalWeightage) * 100.0 : 0;
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); // 👈 Inject Db
                    var existingActivityGrade = await dbContext.ActivityGrade
       .FirstOrDefaultAsync(ag => ag.StudentId == studentId, cancellationToken);

                    if (existingActivityGrade != null)
                    {
                        // Update only, do not create
                        existingActivityGrade.Score = finalGrades[studentId];
                        dbContext.ActivityGrade.Update(existingActivityGrade);
                    }
                    else
                    { 
                    var activityGrade = new ActivityGrade
                    {
                        ActivityGradeId = Guid.NewGuid(),
                        StudentId = studentId,
                      //  ActivityId = studentGroup.FirstOrDefault().ActivityId,
                        Score = finalGrades[studentId],
                    };
                        dbContext.ActivityGrade.Add(activityGrade);
                    }
                 
                   await dbContext.SaveChangesAsync();
                   
            }

            return finalGrades;
            }
        }
    
}


}
