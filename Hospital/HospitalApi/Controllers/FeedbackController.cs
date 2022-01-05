using AutoMapper;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Authorization;
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
    public class FeedbackController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FeedbackController(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            _mapper = mapper;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult GetPublishableFeedbacks()
        {
            try
            {
                var feedbackReadRepo = _uow.GetRepository<IFeedbackReadRepository>();
                var feedbacks = feedbackReadRepo.GetAll().Include(x => x.Patient);
                return Ok(feedbacks);

            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading comments!");
            }
            
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("approved")]
        public IEnumerable<Feedback> GetApprovedFeedbacks()
        {
            var feedbackReadRepo = _uow.GetRepository<IFeedbackReadRepository>();
            return feedbackReadRepo.GetAll().Include(x => x.Patient).Where(x => x.IsPublishable == true && x.FeedbackStatus == FeedbackStatus.Approved);
        }

        [Authorize(Roles = "Patient")]
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
                Feedback addedFeedback = feedbackWriteRepo.Add(_mapper.Map<Feedback>(feedbackDTO));

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

        
        [Authorize(Roles = "Manager")]
        [HttpPut("publish")]
        public IActionResult ApproveFeedback(Feedback feedback)
        {
            try
            {
                if(feedback == null)
                {
                    return BadRequest("Feedback format is wrong!");
                }

                var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();
                feedback.FeedbackStatus = FeedbackStatus.Approved;
                Feedback approvedFeedback = feedbackWriteRepo.Update(feedback);

                if(approvedFeedback == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't update feedback!");
                }

                return Ok(approvedFeedback);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in database!");
            }
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("{Id}")]
        public Feedback GetFeedback(int Id )
        {
            var feedbackReadRepo = _uow.GetRepository<IFeedbackReadRepository>();
            return feedbackReadRepo.GetById(Id);

        }

        [Authorize(Roles = "Manager")]
        [HttpPut("unpublish")]
        public IActionResult UnapproveFeedback(Feedback feedback)
        {
            try
            {
                if (feedback == null)
                {
                    return BadRequest("Feedback format is wrong!");
                }

                var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();
                feedback.FeedbackStatus = FeedbackStatus.Pending;
                Feedback approvedFeedback = feedbackWriteRepo.Update(feedback);

                if (approvedFeedback == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't update feedback!");
                }

                return Ok(approvedFeedback);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in database!");
            }
        }

    }
}
