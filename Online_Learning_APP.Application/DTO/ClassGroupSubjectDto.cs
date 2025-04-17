using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_APP.Application.DTO
{
    public class ClassGroupSubjectDto
    {
        public Guid? ClassGroupSubjectId { get; set; }
        public Guid ClassGroupId { get; set; }
        public string ClassGroupClassName { get; set; }
        public string SubjectSubjectName { get; set; }

        public Guid SubjectId { get; set; }

        //public Subject Subject { get; set; }
    }
}
