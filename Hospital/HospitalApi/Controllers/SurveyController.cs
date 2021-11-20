using Hospital.Model;
using Hospital.Model.Enumerations;
using AutoMapper;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;



namespace HospitalApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class SurveyController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SurveyController(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        public IEnumerable<Survey> GetSurveys()
        {
            var surveyReadRepo = _uow.GetRepository<ISurveyReadRepository>();
            return surveyReadRepo.GetAll().Include(x => x.Questions);

        }
        [HttpGet("{Id}")]
        public Survey GetSurveys(int Id)
        {
            var surveyReadRepo = _uow.GetRepository<ISurveyReadRepository>();
            return surveyReadRepo.GetById(Id);

        }
        [HttpPost]
        public void AnswerSurvey(AnsweredSurveyDTO answeredSurvey)
        {
          //TODO:
        }
        
              
    

    }
}
