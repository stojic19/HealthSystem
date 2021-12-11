using AutoMapper;
using Hospital.Schedule.Model;
using HospitalApi.DTOs;

namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperSurvey : Profile
    {
        public AutoMapperSurvey()
        {
            CreateMap<QuestionDTO, Question>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<SurveyDTO, Survey>();
            CreateMap<Survey, SurveyDTO>();


        }
    }
}