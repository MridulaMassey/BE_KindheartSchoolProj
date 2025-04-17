using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class ActivityGradeDto
    {
        public Guid StudentId { get; set; }
        public Guid ActivityId { get; set; }
        public double Score { get; set; }
    }
}
