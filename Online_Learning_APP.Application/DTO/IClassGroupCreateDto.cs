using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class ClassGroupCreateDto
    {
        public string ClassName { get; set; }
        public Guid AdminId { get; set; } // The admin creating the class
        //public List<Guid>? ActivityIds { get; set; } 
        //public List<Guid>? SubjectIds { get; set; } 
    }

}
