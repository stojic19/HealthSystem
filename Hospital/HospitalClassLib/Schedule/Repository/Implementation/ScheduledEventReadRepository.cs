using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ScheduledEventReadRepository : ReadBaseRepository<int, ScheduledEvent>, IScheduledEventReadRepository
    {
        private readonly AppDbContext _context;

        public ScheduledEventReadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ScheduledEvent> GetDoctorsScheduledEvents(int doctorId)
        {
            return _context.Set<ScheduledEvent>().Where(x => x.DoctorId == doctorId).AsEnumerable();
        }

        public bool IsDoctorAvailableInTerm(int doctorId, DateTime date)
        {
            var scheduledEvents = GetDoctorsScheduledEvents(doctorId);
            return scheduledEvents.Where(s => DateTime.Compare(s.StartDate, date) == 0).All(s => s.IsCanceled);
        }

        public List<ScheduledEvent> GetNumberOfCanceledEventsForPatient(int id)
        {
            return GetAll().Where(x => x.PatientId == id && x.IsCanceled).ToList();
        }

        public List<ScheduledEvent> GetCanceledUserEvents(string userName)
        {
            return GetAll().Include(x => x.Doctor)
                            .Include(x => x.Room)
                            .Include(x => x.Doctor.Specialization)
                            .Where(x => x.IsCanceled && !x.IsDone && x.Patient.UserName.Equals(userName))
                            .ToList();
        }

        public List<ScheduledEvent> GetFinishedUserEvents(string userName)
        {
            return  GetAll().Include(x => x.Doctor)
                            .Include(x => x.Room)
                            .Include(x => x.Doctor.Specialization)
                            .Where(x => x.IsDone && x.Patient.UserName.Equals(userName))
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

        public List<ScheduledEvent> GetUpcomingUserEvents(string userName)
        {
            return GetAll().Include(x => x.Doctor)
                            .Include(x => x.Room)
                            .Include(x => x.Doctor.Specialization)
                            .Where(x => !x.IsCanceled && !x.IsDone && x.Patient.UserName.Equals(userName))
                            .ToList();
        }

        public List<ScheduledEvent> UpdateFinishedUserEvents()
        {
            return GetAll().Where(x => !x.IsDone && !x.IsCanceled && DateTime.Compare(x.EndDate, DateTime.Now) < 0).ToList();
        }
    }
}
