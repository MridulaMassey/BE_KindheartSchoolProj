using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class PrintableResource
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string? Description { get; set; }
        public string FileUrl { get; set; }

        // Renamed from UploadedByTeacherId to TeacherId
        public Guid TeacherId { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public Guid? ClassGroupId { get; set; }
        public Guid? SubjectId { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public Teacher? Teacher { get; set; }
        public ClassGroup? ClassGroup { get; set; }
        public Subject? Subject { get; set; }

    }
}
