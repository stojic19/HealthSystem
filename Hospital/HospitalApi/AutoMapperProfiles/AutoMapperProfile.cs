using AutoMapper;
using Hospital.Model;
using Hospital.Model.Enumerations;
using HospitalApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NewFeedbackDTO, Feedback>()
                  .ForMember(dto => dto.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                  .ForMember(dto => dto.FeedbackStatus, opt => opt.MapFrom(src => FeedbackStatus.Pending));
        }
    }
}
