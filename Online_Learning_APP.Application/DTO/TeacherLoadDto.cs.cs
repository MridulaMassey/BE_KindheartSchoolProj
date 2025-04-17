using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class TeacherLoadDto
    {
        public Guid TeacherId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int ClassGroupCount { get; set; }
        public List<string> ClassGroupNames { get; set; }

    }
}
