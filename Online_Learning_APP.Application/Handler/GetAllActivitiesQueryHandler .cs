using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_APP.Application.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.Handler
{
  
    public class GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, List<ClassGroupSubjectStudentActivityDto>>
    {
        private readonly IClassGroupSubjectStudentActivityRepository _repository;
        private readonly IMapper _mapper;

        public GetAllActivitiesQueryHandler(IClassGroupSubjectStudentActivityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ClassGroupSubjectStudentActivityDto>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
        {
            var activities = await _repository.GetAllAsync(); // Assumes a method like this exists
            var result = _mapper.Map<List<ClassGroupSubjectStudentActivityDto>>(activities);
            return result;
        }
    }

}
