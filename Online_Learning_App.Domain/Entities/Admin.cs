using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class Admin
    {
        public Guid AdminId { get; set; }  // Primary Key

        public string? Email { get; set; } = null;
        public string? UserName { get; set; } = null;

        public Guid? RoleId { get; set; }  // Foreign Key to Role
        public virtual Role Role { get; set; }

        public Guid UserId { get; set; }  // Foreign Key to ApplicationUser
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        // Admin-specific functionalities (e.g., managing teachers, classes, etc.)
        //public ICollection<Teacher> ManagedTeachers { get; set; } = new List<Teacher>();
        //  public ICollection<ClassGroup> ManagedClassGroups { get; set; } = new List<ClassGroup>();
        public ICollection<ClassGroup> ClassGroups { get; set; } = new List<ClassGroup>();
    }
}
