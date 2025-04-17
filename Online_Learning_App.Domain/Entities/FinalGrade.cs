using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class FinalGrade
    {
        [Key]
        public Guid FinalGradeId { get; set; }

        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        [ForeignKey("Subject")]
        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public double FinalScore { get; set; } // Weighted score across all activities
        public string Grade { get; set; } // A, B, C, etc.

        public bool IsReleased { get; set; } // If the teacher has officially released it
    }
}
