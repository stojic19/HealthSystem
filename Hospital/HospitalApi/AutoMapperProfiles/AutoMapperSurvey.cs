using AutoMapper;
using Hospital.Model;
using HospitalApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
