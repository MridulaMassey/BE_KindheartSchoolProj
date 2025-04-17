using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class StudentActivityWithSubmissionDto
    {
        public Guid ActivityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string PdfUrl { get; set; }

        // Submission Info
        public bool IsSubmitted { get; set; }
        public string SubmissionUrl { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string? Feedback { get; set; }
        public int? Grade { get; set; }

        //added new comlumn for student comment
        public string? StudentComment { get; set; }
    }
}
