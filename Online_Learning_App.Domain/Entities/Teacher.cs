using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class Teacher
    {
        //public Guid RoleId { get; set; } // Link to Role
        //public Role Role { get; set; }
        public Guid Id { get; set; }

        public string? Email { get; set; } = null;
        public string? UserName { get; set; } = null;
        public virtual Role Role { get; set; }
        public Guid? RoleId { get; set; }
        public Guid UserId { get; set; } // Foreign key to ApplicationUser
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        //  public ICollection<Teacher> ManagedTeachers { get; set; } = new List<Teacher>();
        public ICollection<ClassGroup> ManagedClassGroups { get; set; } = new List<ClassGroup>();
    }
}
