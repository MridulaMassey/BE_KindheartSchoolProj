using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class AssignTeacherDto
    {
        public Guid ClassGroupId { get; set; }
        public Guid TeacherId { get; set; }
    }
}

