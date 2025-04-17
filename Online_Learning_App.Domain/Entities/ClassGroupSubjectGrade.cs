using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class ClassGroupSubjectGrade
    {
        public Guid ClassGroupId { get; set; }
        public ClassGroup ClassGroup { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public Guid GradeId { get; set; }
        public Grade Grade { get; set; }

        public decimal ObtainedMarks { get; set; }
    }
}
