using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class UpdateActivityDto
    {
        public string ActivityId { get; set; }
        public string FileBase64 { get; set; }
        public string FileName { get; set; }
        public string? StudentId { get; set; }

        //for teacher
        //public string ActivityName { get; set; }
        //public string Description { get; set; }

    }
}
