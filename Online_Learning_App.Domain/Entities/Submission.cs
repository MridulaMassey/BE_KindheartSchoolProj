using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    // Domain Entity: Submission
    public class Submission
    {
        public Guid SubmissionId { get; set; }

        // Foreign key relationship to Activity
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }

        // Foreign key relationship to Student
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public string PdfUrl { get; set; }
        public DateTime SubmissionDate { get; set; }
        public bool IsReviewed { get; set; } = false;
        public string Feedback { get; set; }
        public int Grade { get; set; }
     
        public string StudentComment { get; set; }
        public bool IsSubmitted { get; set; } = false;
    }
}
