using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Online_Learning_APP.Application.DTO;
namespace Online_Learning_APP.Application.Handler
{
    public class CreateActivityCommand : IRequest<ClassGroupSubjectStudentActivityDto>
    {
        public Guid ClassGroupSubjectId { get; set; }
        public Guid ActivityId { get; set; }
        public Guid StudentId { get; set; }

        public Guid ClassGroupSubjectStudentActivityId { get; set; }

        // Optional: Add validation here or use FluentValidation separately
    }
}
