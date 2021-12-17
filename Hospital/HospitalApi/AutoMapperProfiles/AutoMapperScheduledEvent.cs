using AutoMapper;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Enumerations;
using HospitalApi.DTOs;


namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperScheduledEvent : Profile
    {
        public AutoMapperScheduledEvent()
        {
            CreateMap<ScheduledEvent, ScheduledEventsDTO>();

            CreateMap<ScheduleAppointmentDTO, ScheduledEvent>()
                .ForMember(toSchedule => toSchedule.EndDate, opt => opt.MapFrom(src => src.StartDate.AddHours(1)))
                .ForMember(toSchedule => toSchedule.IsCanceled, opt => opt.MapFrom(src => false))
                .ForMember(toSchedule => toSchedule.IsDone, opt => opt.MapFrom(src => false))
                .ForMember(toSchedule => toSchedule.ScheduledEventType,
                    opt => opt.MapFrom(src => ScheduledEventType.Appointment)).ReverseMap();

        }
    }
}