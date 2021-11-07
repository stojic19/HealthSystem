using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Services
{
    public interface IFeedbackService
    {
        public  IEnumerable<Feedback> GetFeedbacksForApproval();

        public void InsertFeedback(Feedback feedback);
        
    }
}
