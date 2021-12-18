using AutoMapper;
using Hospital.Schedule.Model;
using HospitalApi.DTOs;
using System;

namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperAnsweredSurvey : Profile
    {
        public AutoMapperAnsweredSurvey()
        {
            CreateMap<AnsweredQuestionDTO, AnsweredQuestion>()
                .ForMember(o => o.Category, opt => opt.MapFrom(src => src.Type));

            CreateMap<AnsweredSurveyDTO, AnsweredSurvey>()
               .ForMember(o => o.AnsweredDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(o => o.AnsweredQuestions, opt => opt.MapFrom(src => src.questions));

        }

    }
}