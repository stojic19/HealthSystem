using System;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;
using System.Linq;
using Hospital.Schedule.Service.Interfaces;

namespace Hospital.Schedule.Service
{
    public class ScheduledEventService : IScheduledEventService
    {
        private readonly IUnitOfWork UoW;
        private const int StartHour = 7;
        private const int EndHour = 15;
        public ScheduledEventService(IUnitOfWork UoW)
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

        public IEnumerable<DateTime> GetAvailableAppointments(int doctorId, DateTime preferredDate)
        {
            var availableTerms = new List<DateTime>();
            for (var date = preferredDate.AddHours(StartHour); date < preferredDate.AddHours(EndHour); date = date.AddHours(1))
            {
                if(UoW.GetRepository<IScheduledEventReadRepository>().IsDoctorAvailableInTerm(doctorId, date))
                        availableTerms.Add(date);
            }
            return availableTerms;
        }
    }
}
