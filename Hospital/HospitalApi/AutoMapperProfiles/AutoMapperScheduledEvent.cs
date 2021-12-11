using AutoMapper;
using Hospital.Schedule.Model;
using HospitalApi.DTOs;


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