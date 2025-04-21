using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class StudentProfileDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class ClassGroupWithStudentsDto
    {
        public Guid ClassGroupId { get; set; }
        public string ClassName { get; set; }
        public List<StudentProfileDto> Students { get; set; }
    }
}