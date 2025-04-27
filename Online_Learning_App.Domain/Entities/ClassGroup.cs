using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{

    // Domain Entity: ClassGroup (NEW - Replaces ClassLevel)
    public class ClassGroup
    {
        public Guid ClassGroupId { get; set; }
        public string ClassName { get; set; } // Example: "Year 4 - Section A"

        // Foreign Key to Teacher
        public Guid? AdminId { get; set; } = null;
        [JsonIgnore]
        public virtual Admin Admin { get; set; }

        //Teacher in the class
        public Guid? TeacherId { get; set; }

        // Students in the class
        public ICollection<Student> Students { get; set; } = new List<Student>();

        // Activities assigned to this class
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

        // Many-to-Many Relationship with Subjects
        public ICollection<ClassGroupSubject> ClassGroupSubjects { get; set; } = new List<ClassGroupSubject>();

    }
}
