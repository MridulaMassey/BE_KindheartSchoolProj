using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Online_Learning_APP.Application.DTO;
using System.Collections.Generic;

namespace Online_Learning_APP.Application.Handler
{
   

    public class GetAllActivitiesQuery : IRequest<List<ClassGroupSubjectStudentActivityDto>>
    {
        // Optionally add filters if needed (e.g., ClassGroupSubjectId, StudentId)
    }

}
