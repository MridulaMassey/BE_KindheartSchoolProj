using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class PrintableResourceDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public Guid TeacherId { get; set; }
        public Guid? ClassGroupId { get; set; }
        public Guid? SubjectId { get; set; }
        public DateTime UploadDate { get; set; } // date uploaded

    }
}
