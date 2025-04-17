using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class Certificate
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public string CertificateUrl { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid? TeacherId { get; set; } // Optional
        public Teacher? Teacher { get; set; }

        public DateTime DateIssued { get; set; }

        public bool IsRevoked { get; set; } = false;
        public void Revoke()
        {
            IsRevoked = true;
        }


    }

}
