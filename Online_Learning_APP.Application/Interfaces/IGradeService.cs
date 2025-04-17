using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IGradeService
    {
        Task AssignGradeToActivityTeacher(ActivityGradeDto activityGradeDto);
        Task AssignGradeToActivity(ActivityGradeDto activityGradeDto);
        Task<double> CalculateFinalGrade(FinalGradeDto finalGradeDto);
        Task<bool> ReleaseFinalGrade(FinalGradeDto finalGradeDto);
    }
}
