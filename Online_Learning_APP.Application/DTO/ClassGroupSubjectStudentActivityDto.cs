using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Online_Learning_APP.Application.DTO
{
    public class ClassGroupSubjectStudentActivityDto
    {
    
        public Guid ActivityId { get; set; }

        public Guid StudentId { get; set; }
        public string StudentUsername { get; set; }
      //  public Activity Activity { get; set; }

        public string ActivityActivityName { get; set; }

        public string ClassGroupSubjectClassGroupClassName { get; set; }

        public string ActivityTeacherUserFirstName { get; set; }
        public string ActivityTeacherUserLastName { get; set; }

        public string ActivityWeightagePercent{ get; set; }

        public string ActivityTitle { get; set; }
        public string ActivityDescription{ get; set; }
        public string ActivityDueDate { get; set; }
        
        public string Feedback { get; set; }
        public string pdfUrl { get; set; }
        //        activityName
        //subjectName
        //classGroupName
        //classLevel
        //dueDate(You might want the raw date string or the formatted version)
        //teacherUserFirstName
        //teacherUserLastName
        //weightagePercent
        //title(Activity Title within the details)
        //description
        //feedback(If you want to include teacher feedback)
        //pdfUrl



    }
}
