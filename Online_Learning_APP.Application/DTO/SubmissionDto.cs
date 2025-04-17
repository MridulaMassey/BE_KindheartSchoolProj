using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Online_Learning_APP.Application.DTO
{
    public class SubmissionDto  //Student Submission
    {
        public Guid ActivityId { get; set; }
        public Guid StudentId { get; set; }
        public string PdfUrl { get; set; } // From Google Cloud
      
        public string StudentComment { get; set; } //Student comment



    }
}
