using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class SubjectGrade
    {
        [Key]  
        public Guid SubjectGradeId { get; set; }  // Primary Key
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public Guid GradeId { get; set; }
        public Grade Grade { get; set; }

        public bool IsOverallGrade { get; set; }
        public decimal MaxMarks { get; set; }
    }
}
