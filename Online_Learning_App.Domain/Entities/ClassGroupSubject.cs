using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class ClassGroupSubject
    {
        public Guid ClassGroupSubjectId { get; set; }
        public Guid ClassGroupId { get; set; }
        public ClassGroup ClassGroup { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
