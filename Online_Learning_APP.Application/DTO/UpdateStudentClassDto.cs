using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class UpdateStudentClassDto
    {
        public string UserName { get; set; }
        public string ClassLevel { get; set; }
        public Guid ClassGroupId { get; set; }

    }
}
