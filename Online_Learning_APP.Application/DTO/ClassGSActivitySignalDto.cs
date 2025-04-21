using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class ClassGSActivitySignalDto
    {
        public Guid ClassGroupSubjectActivityId { get; set; }
        public Guid ClassGroupSubjectId { get; set; }
        public Guid ActivityId { get; set; }
    }
}
