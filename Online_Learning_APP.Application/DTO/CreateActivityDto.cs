using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Online_Learning_APP.Application.DTO
{
    public class CreateActivityDto
    {
        
            public string ActivityName { get; set; }
            public Guid? ClassGroupId { get; set; }
        public Guid SubjectId
        {
            get; set;
        }
            public string Description { get; set; }
        public string? ClassGroupClassName { get; set; } = null;

        public string? ClassLevel { get; set; } = null;
            public DateTime DueDate { get; set; }
            public string FileName { get; set; }
            public string PdfFileBase64 { get; set; }
            public Guid TeacherId { get; set; }
            public string Title { get; set; }
            public double WeightagePercent { get; set; } // Weightage per activity
        public Boolean? HasFeedback { get; set; } = false;
        public string? Feedback { get; set; }


    }
}
