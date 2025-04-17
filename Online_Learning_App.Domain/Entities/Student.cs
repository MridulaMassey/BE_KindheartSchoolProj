using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class Student 
    {
        public Guid Id { get; set; }
     
        public string? Email { get; set; } = null;
        public string? UserName { get; set; } = null;
        public virtual Role Role { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? ClassGroupId { get; set; } // Allow NULL if student is not assigned to a class
        public virtual ClassGroup? ClassGroup
        {
            get; set;
        }

        public Guid UserId { get; set; } // Foreign key to ApplicationUser
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        //public Guid RoleId { get; set; } // Link to Role
        //public Role Role { get; set; }
        public string ClassLevel { get; set; }
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
