using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service.ServiceInterface;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Schedule.Service
{
    public class ScheduledEventsService : IScheduledEventsService
    {
        private readonly IUnitOfWork UoW;
        public ScheduledEventsService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }
        public List<ScheduledEvent> getFinishedUserEvents(int userId)
        {
            return UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                        .Where(x => x.IsDone && x.Patient.Id == userId)
                        .ToList();
        }

        public int getNumberOfFinishedEvents(int userId)
        {
            var count = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                        .Where(x => x.IsDone && x.Patient.Id == userId)
                        .GroupBy(t => t.Patient)
                        .Select(g => g.Count());

            return count.FirstOrDefault();
        }

        public void updateFinishedUserEvents()
        {
      
             UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                         .Where(x => x.IsDone==false &&  DateTime.Compare(x.EndDate,DateTime.Now) < 0)
                         .ToList()
                         .ForEach(one => one.IsDone = true);

        }
    }
}
