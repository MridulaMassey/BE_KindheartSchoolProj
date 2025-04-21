using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Infrastructure;
using Online_Learning_APP.Application.DTO;
using MediatR;
using Online_Learning_App.Domain.Interfaces;

namespace Online_Learning_APP.Application.Handler
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, ClassGroupSubjectStudentActivityDto>
    {
        //private readonly IApplicationDbContext _context;
        private readonly IClassGroupSubjectStudentActivityRepository _classgroupsubjectstudentActivityrepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ActivityHub> _hubContext;

        public CreateActivityCommandHandler(IMapper mapper, IHubContext<ActivityHub> hubContext, IClassGroupSubjectStudentActivityRepository classgroupsubjectstudentActivityrepository)
        {
           // _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
            _classgroupsubjectstudentActivityrepository = classgroupsubjectstudentActivityrepository;
        }

        public async Task<ClassGroupSubjectStudentActivityDto> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = new ClassGroupSubjectStudentActivity
            {
                ClassGroupSubjectStudentActivityId = request.ClassGroupSubjectStudentActivityId,
                ClassGroupSubjectId = request.ClassGroupSubjectId,
                ActivityId = request.ActivityId,
                StudentId=request.StudentId,
            };

            await _classgroupsubjectstudentActivityrepository.AddAsync(entity);
            //.Add(entity);
        //    await _context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ClassGroupSubjectStudentActivityDto>(entity);

            // Broadcast the new activity
            await _hubContext.Clients.All.SendAsync("ReceiveActivity", dto);

            return dto;
        }
    }

}
