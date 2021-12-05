using AutoMapper;
using Hospital.Model;
using HospitalApi.DTOs;
namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperAnsweredQuestion : Profile
    {
        public AutoMapperAnsweredQuestion()
        {
            CreateMap<AnsweredQuestion, AnsweredQuestionDTO>();
            CreateMap<AnsweredQuestionDTO, AnsweredQuestion>();
        }
    }
}
