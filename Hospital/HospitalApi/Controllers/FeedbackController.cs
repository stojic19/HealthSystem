using AutoMapper;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
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
using Hospital.SharedModel.Model.Enumerations;

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
            return feedbackReadRepo.GetAll().Include(x => x.Patient).Where(x => x.IsPublishable && x.FeedbackStatus == FeedbackStatus.Approved);
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult InsertFeedback(NewFeedbackDTO feedbackDTO)
        {
            var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();
            var loggedInPatient = _uow.GetRepository<IPatientReadRepository>().GetAll().First(x => x.UserName.Equals(feedbackDTO.PatientUsername));
            feedbackDTO.PatientId = loggedInPatient.Id;
            var newFeedback = _mapper.Map<Feedback>(feedbackDTO);
            return feedbackWriteRepo.Add(newFeedback) == null ? StatusCode(StatusCodes.Status500InternalServerError, "Could not insert feedback in the database.")
                : Ok("Your feedback has been submitted.");

        }

        
        [Authorize(Roles = "Manager")]
        [HttpPut("publish")]
        public IActionResult ApproveFeedback(FeedbackDTO feedback)
        {
            try
            {
                if(feedback == null)
                    return BadRequest("Feedback format is wrong!");
                var feedbackFromDb = (_uow.GetRepository<IFeedbackReadRepository>().GetAll().Where(x => x.Id == feedback.Id).Include(x => x.Patient)).FirstOrDefault();
                var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();
                feedbackFromDb.Publish();
                var approvedFeedback = feedbackWriteRepo.Update(feedbackFromDb);
                var approvedFeedbackDto = _mapper.Map<FeedbackDTO>(approvedFeedback);
                return approvedFeedbackDto == null ? StatusCode(StatusCodes.Status500InternalServerError, "Couldn't update feedback!")
                    : Ok(approvedFeedbackDto);
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
        public IActionResult UnapproveFeedback(FeedbackDTO feedback)
        {
            try
            {
                if (feedback == null)
                    return BadRequest("Feedback format is wrong!");
                var feedbackFromDb = (_uow.GetRepository<IFeedbackReadRepository>().GetAll().Where(x => x.Id == feedback.Id).Include(x => x.Patient)).FirstOrDefault();
                var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();
                feedbackFromDb.Unpublish();
                var unapprovedFeedback = feedbackWriteRepo.Update(feedbackFromDb);
                var unapprovedFeedbackDto = _mapper.Map<FeedbackDTO>(unapprovedFeedback);
                return unapprovedFeedbackDto == null ? StatusCode(StatusCodes.Status500InternalServerError, "Couldn't update feedback!")
                    : Ok(unapprovedFeedbackDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in database!");
            }
        }

    }
}
