using Hospital.Model;
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
        public IActionResult GetPublishableFeedbacks()
        {
            try
            {
                var feedbackReadRepo = _uow.GetRepository<IFeedbackReadRepository>();
                var feedbacks = feedbackReadRepo.GetAll().Include(x => x.Patient).Where(x => x.IsPublishable == true);
                return Ok(feedbacks);

            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading comments!");
            }
            
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

        [HttpGet("{Id}")]
        public Feedback GetFeedback(int Id )
        {
            var feedbackReadRepo = _uow.GetRepository<IFeedbackReadRepository>();
            return feedbackReadRepo.GetById(Id);

        }

    }
}
