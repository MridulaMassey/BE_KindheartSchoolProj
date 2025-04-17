using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class CreateCertificateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string CertificateUrl { get; set; }
        public Guid StudentId { get; set; }
        public Guid? TeacherId { get; set; }
    }
}
