using Hospital.Model;
using Hospital.Model.Enumerations;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Model.Enumerations;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class FeedbackController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public FeedbackController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet]
        public IEnumerable<Feedback> GetFeedbacks()
        {
            var feedbackReadRepo = _uow.GetRepository<IFeedbackReadRepository>();

            return feedbackReadRepo.GetAll().Include(x => x.Patient).Where(x => x.IsPublishable == true);
        }
        [HttpGet("approved")]
        public IEnumerable<Feedback> GetApprovedFeedbacks()
        {
            var feedbackReadRepo = _uow.GetRepository<IFeedbackReadRepository>();
            return feedbackReadRepo.GetAll().Include(x => x.Patient).Where(x => x.IsPublishable == true && x.FeedbackStatus == FeedbackStatus.Approved);
        }

        [HttpPost]
        public void InsertFeedback(Feedback feedback)
        {
            var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();

            feedbackWriteRepo.Add(feedback);
        }

        [HttpPut]
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
        [HttpGet("{Id}")]
        public Feedback GetFeedback(int Id )
        {
            var feedbackReadRepo = _uow.GetRepository<IFeedbackReadRepository>();
            return feedbackReadRepo.GetById(Id);

        }

    }
}
