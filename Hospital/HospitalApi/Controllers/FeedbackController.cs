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

        [HttpPost]
        public void InsertFeedback(Feedback feedback)
        {
            var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();

            feedbackWriteRepo.Add(feedback);
        }

        [HttpPut]
        public async Task<ActionResult<Feedback>> ApproveFeedback(Feedback feedback)
        {
            var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();
            try
            {
                if(feedback == null)
                {
                    return BadRequest();
                }
                feedback.FeedbackStatus = FeedbackStatus.Approved;
                return feedbackWriteRepo.Update(feedback);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in database!");
            }
        }
    }
}
