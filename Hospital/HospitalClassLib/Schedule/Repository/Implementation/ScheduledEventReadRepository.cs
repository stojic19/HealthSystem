using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ScheduledEventReadRepository : ReadBaseRepository<int, ScheduledEvent>, IScheduledEventReadRepository
    {
     
        public ScheduledEventReadRepository(AppDbContext context) : base(context)
        {
        
        }

        public List<ScheduledEvent> GetCanceledUserEvents(int userId)
        {
            return GetAll().Include(x => x.Doctor)
                            .Include(x => x.Room)
                            .Include(x => x.Doctor.Specialization)
                            .Where(x => x.IsCanceled && !x.IsDone && x.Patient.Id == userId)
                            .ToList();
        }

        public List<ScheduledEvent> GetFinishedUserEvents(int userId)
        {
            return  GetAll().Include(x => x.Doctor)
                            .Include(x => x.Room)
                            .Include(x => x.Doctor.Specialization)
                            .Where(x => x.IsDone && x.Patient.Id == userId)
                            .ToList();
        }

        public int GetNumberOfFinishedEvents(int userId)
        {
            return  GetAll().Where(x => x.IsDone && x.Patient.Id == userId)
                            .GroupBy(t => t.Patient)
                            .Select(g => g.Count()).FirstOrDefault();
        
        }

        public ScheduledEvent GetScheduledEvent(int eventId)
        {
            return GetAll().Where(x => x.Id == eventId)
                            .Include(x => x.Room)
                            .Include(x => x.Doctor.Specialization).FirstOrDefault();
        }

        public List<ScheduledEvent> GetUpcomingUserEvents(int userId)
        {
            return GetAll().Include(x => x.Doctor)
                            .Include(x => x.Room)
                            .Include(x => x.Doctor.Specialization)
                            .Where(x => !x.IsCanceled && !x.IsDone && x.Patient.Id == userId)
                            .ToList();
        }

        public List<ScheduledEvent> UpdateFinishedUserEvents()
        {
            return GetAll().Where(x => !x.IsDone && !x.IsCanceled && DateTime.Compare(x.EndDate, DateTime.Now) < 0).ToList();
        }
    }
}
