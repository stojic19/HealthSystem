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
        public IActionResult InsertFeedback(NewFeedbackDTO feedbackDTO)
        {
            try
            {
                if (feedbackDTO == null)
                {
                    return BadRequest("Incorrect feedback format sent! Please try again.");
                }

                var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();
                Feedback addedFeedback = feedbackWriteRepo.Add(createFeedback(feedbackDTO));

                if(addedFeedback == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Could not insert feedback in the database.");
                }

                return Ok("Your feedback has been submitted.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting feedback in the database.");
            }
        }
        private Feedback createFeedback(NewFeedbackDTO feedbackDTO)
        {
            Feedback newFeedback = _mapper.Map<Feedback>(feedbackDTO);
            newFeedback.CreatedDate = DateTime.Now;
            newFeedback.FeedbackStatus = Hospital.Model.Enumerations.FeedbackStatus.Pending;
            return newFeedback;
        }
    }
}
