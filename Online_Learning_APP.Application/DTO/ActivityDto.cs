using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Online_Learning_App.Domain.Entities;
using Activity = System.Diagnostics.Activity;

namespace Online_Learning_APP.Application.DTO
{
   // [AutoMap(typeof(Activity))]
        public class ActivityDto
    {
        public Guid ActivityId { get; set; }

        /// <summary>
        /// dto
        /// </summary>
        public string Title { get; set; }
        public string Description { get; set; }
        public string PdfUrl { get; set; }
        public DateTime DueDate { get; set; }
        public string ClassLevel { get; set; }
        public Guid TeacherId { get; set; }
        public string TeacherUserFirstName { get; set; }
        public string TeacherUserLastName { get; set; }
        public Guid? ClassGroupId { get; set; }
        public Boolean? HasFeedback { get; set; } = false;
        public string? Feedback { get; set; }
        public double WeightagePercent { get; set; } // Weightage per activity 90

        public string StudentPdfUrl { get; set; }
        
    }
}
