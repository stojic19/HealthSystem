using AutoMapper;
using Hospital.Model;
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
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class FeedbackController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FeedbackController(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Feedback> GetFeedbacks()
        {
            var feedbackReadRepo = _uow.GetRepository<IFeedbackReadRepository>();

            return feedbackReadRepo.GetAll().Include(x => x.Patient).Where(x => x.IsPublishable == true);
        }

        [HttpPost]
        public void InsertFeedback(NewFeedbackDTO feedbackDTO)
        {
            var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();
            Feedback newFeedback = _mapper.Map<Feedback>(feedbackDTO);
            newFeedback.CreatedDate = DateTime.Now;
            newFeedback.FeedbackStatus = Hospital.Model.Enumerations.FeedbackStatus.Pending;

            feedbackWriteRepo.Add(newFeedback);
        }
    }
}
