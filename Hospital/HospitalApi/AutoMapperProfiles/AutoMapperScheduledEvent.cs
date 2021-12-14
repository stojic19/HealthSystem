using AutoMapper;
using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
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

            

        }
    }
}