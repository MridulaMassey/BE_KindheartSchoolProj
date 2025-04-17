using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class Grade
    {
        public Guid GradeId { get; set; }
        public string GradeName { get; set; }  // e.g., A+, A, A-, etc.
        public string Classification { get; set; }  // e.g., "1st Class Pass", "2nd Class Pass", "Pass", etc.
        public decimal MinMarks { get; set; }  // Minimum marks for the grade
        [Column(TypeName = "decimal(5,2)")]
        public decimal MaxMarks { get; set; }  // Maximum marks for the grade
       // Precision & Scale defined
      
    }
}
