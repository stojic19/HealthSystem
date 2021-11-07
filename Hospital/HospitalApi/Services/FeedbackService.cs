using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _uow;

        public FeedbackService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<Feedback> GetFeedbacksForApproval()
        {
            var feedbackReadRepo = _uow.GetRepository<IFeedbackReadRepository>();

            return feedbackReadRepo.GetAll().Include(x => x.Patient).Where(x => x.IsPublishable == true);

        }

        public void InsertFeedback(Feedback feedback)
        {
            var feedbackWriteRepo = _uow.GetRepository<IFeedbackWriteRepository>();

            feedbackWriteRepo.Add(feedback);
        }

    }
}
