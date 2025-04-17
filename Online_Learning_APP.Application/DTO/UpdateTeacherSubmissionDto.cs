using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class UpdateTeacherSubmissionDto
    {
        public Guid ActivityId { get; set; }
        public string Feedback { get; set; }
        public double? Grade { get; set; }
    }
}
