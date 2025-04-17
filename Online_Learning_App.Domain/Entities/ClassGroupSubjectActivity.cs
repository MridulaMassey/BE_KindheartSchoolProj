using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class ClassGroupSubjectActivity
    {
        [Key]
        public Guid ClassGroupSubjectActivityId { get; set; }

      
        [ForeignKey("ClassGroupSubject")]
        public Guid ClassGroupSubjectId { get; set; }
        public virtual ClassGroupSubject ClassGroupSubject { get; set; }

     
        [ForeignKey("Activity")]
        public Guid ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        //[ForeignKey("Student")]
        //public Guid? StudentId { get; set; }
        //public virtual Student Student { get; set; }

        
    }
}
