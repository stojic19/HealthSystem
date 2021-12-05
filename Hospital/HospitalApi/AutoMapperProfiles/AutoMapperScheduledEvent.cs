using AutoMapper;
using Hospital.Model;
using HospitalApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperScheduledEvent : Profile
    {
        public AutoMapperScheduledEvent()
        {
            CreateMap<ScheduledEvent, ScheduledEventsDTO>();
               
        }
    }
}
