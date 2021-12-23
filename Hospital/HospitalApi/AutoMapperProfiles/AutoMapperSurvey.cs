using AutoMapper;
using Hospital.Schedule.Model;
using HospitalApi.DTOs;

namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperSurvey : Profile
    {
        public AutoMapperSurvey()
        {
            CreateMap<QuestionDTO, Question>().ReverseMap();
            CreateMap<SurveyDTO, Survey>().ReverseMap();

        }
    }
}