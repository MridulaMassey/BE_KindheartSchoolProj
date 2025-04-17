using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Online_Learning_App.Domain.Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public virtual Role Role { get; set; }
        public Guid? RoleId { get; set; }
        public string? FirstName { get; set; }=null ;
        public string? LastName  { get; set; }=null;

        public virtual  Student Student { get; set; }
        // public virtual Teacher Teacher { get; set; }

        public Guid? ClassgroupId { get; set; }

        public virtual ClassGroup ClassGroup { get; set; } //added
        // public virtual Teacher Teacher { get; set; }
    }
}
