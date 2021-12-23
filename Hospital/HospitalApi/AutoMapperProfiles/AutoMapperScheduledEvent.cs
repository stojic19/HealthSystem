using AutoMapper;
using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.SharedModel.Model.Enumerations;
using HospitalApi.DTOs;


namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperScheduledEvent : Profile
    {
        public AutoMapperScheduledEvent()
        {
            CreateMap<ScheduledEvent, ScheduledEventsDTO>()
                .ForMember(dto => dto.DoctorName, opt => opt.MapFrom(src => src.Doctor.FirstName))
                .ForMember(dto => dto.DoctorLastName, opt => opt.MapFrom(src => src.Doctor.LastName))
                .ForMember(dto => dto.SpecializationName, opt => opt.MapFrom(src => src.Doctor.Specialization.Name))
                .ForMember(dto => dto.RoomName, opt => opt.MapFrom(src => src.Room.Name)).ReverseMap();

            CreateMap<EventForSurvey, EventsForSurveyDTO>()
                 .ForMember(dto => dto.scheduledEventsDTO, opt => opt.MapFrom(src => src.scheduledEvent))
                .ForMember(dto => dto.answeredSurveyId, opt => opt.MapFrom(src => src.answeredSurveyId)).ReverseMap();

            

            CreateMap<ScheduleAppointmentDTO, ScheduledEvent>()
                .ForMember(toSchedule => toSchedule.EndDate, opt => opt.MapFrom(src => src.StartDate.AddHours(1)))
                .ForMember(toSchedule => toSchedule.IsCanceled, opt => opt.MapFrom(src => false))
                .ForMember(toSchedule => toSchedule.IsDone, opt => opt.MapFrom(src => false))
                .ForMember(toSchedule => toSchedule.ScheduledEventType,
                    opt => opt.MapFrom(src => ScheduledEventType.Appointment)).ReverseMap();

        }
    }
}