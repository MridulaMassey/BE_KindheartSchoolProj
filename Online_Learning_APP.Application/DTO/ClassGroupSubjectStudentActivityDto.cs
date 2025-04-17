using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_APP.Application.DTO
{
    public class ClassGroupSubjectStudentActivityDto
    {
    
        public Guid ActivityId { get; set; }

        public Guid StudentId { get; set; }
        public string StudentUsername { get; set; }

        public string ActivityActivityName { get; set; }

        public string ClassGroupSubjectClassGroupClassName { get; set; }
          



    }
}
