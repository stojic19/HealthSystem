using Hospital.Model;
using HospitalApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public IEnumerable<Feedback> GetFeedbacks()
        {
            IEnumerable<Feedback> feeds = _feedbackService.GetFeedbacksForApproval();
            Console.WriteLine(feeds.Count());
            return feeds;
        }

        [HttpPost]
        public void InsertFeedback(Feedback feedback)
        {
            _feedbackService.InsertFeedback(feedback);
        }
    }
}
