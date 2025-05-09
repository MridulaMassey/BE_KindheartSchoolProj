﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_APP.Application.DTO
{
    public class MappingProfile : Profile

    {

        public MappingProfile()

        {

            CreateMap<CreateActivityDto, Activity>();
            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Grade, ActivityGradeDto>().ReverseMap();
            CreateMap<ClassGroupSubject, ClassGroupSubjectDto>().ReverseMap();
            CreateMap<ClassGroupSubjectStudentActivity, ClassGroupSubjectStudentActivityDto>().ReverseMap();
            CreateMap<ClassGSActivitySignalDto, ClassGroupSubjectStudentActivityDto>().ReverseMap();


            CreateMap<ClassGroupSubjectStudentActivity, NotificationDto>().ReverseMap();
           // CreateMap<updateNotificationDto, NotificationDto>().ReverseMap();
            CreateMap<updateNotificationDto, ClassGroupSubjectStudentActivity>().ReverseMap();
            CreateMap<ActivityGradeDto, ActivityGrade>().ReverseMap();
            //   CreateMap<ClassGroupSubjectStudentActivity, ClassGroupSubjectStudentActivityDto>();
            // Add other mappings as needed

        }

    }
}