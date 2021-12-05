using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services.ServiceImpl
{
    public class ScheduledEventsService : IScheduledEventsService
    {
        private IUnitOfWork UoW;

        public ScheduledEventsService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

 
        public List<ScheduledEvent> getFinishedUserEvents(int userId)
        {
           
            var events = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                .Where(x => x.IsDone == true && x.Patient.Id == userId).ToList();

            return events;

        }
        public int getNumberOfFinishedEvents(int userId)
        {
            /*
                SELECT count("Id") FROM public."ScheduledEvents"
               WHERE "IsDone" = true
               GROUP BY "PatientId" 

            */
            var count = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                .Where(x => x.IsDone == true && x.Patient.Id == userId)
                .GroupBy(t => t.Patient)
                .Select(g => g.Count());

            return count.FirstOrDefault();
        }

        }
}

