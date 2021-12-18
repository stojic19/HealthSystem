using AutoMapper;
using Hospital.Schedule.Model;
using HospitalApi.DTOs;
namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperAnsweredQuestion : Profile
    {
        public AutoMapperAnsweredQuestion()
        {
            CreateMap<AnsweredQuestion, AnsweredQuestionDTO>().ReverseMap();
        }
    }
}