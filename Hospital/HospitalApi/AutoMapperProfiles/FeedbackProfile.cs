using AutoMapper;
using Hospital.Schedule.Model;
using HospitalApi.DTOs;

namespace HospitalApi.AutoMapperProfiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<NewFeedbackDTO, Feedback>();
        }
    }
}
