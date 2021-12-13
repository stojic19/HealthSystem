using System;
using System.Collections.Generic;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository
{
    public interface IScheduledEventReadRepository : IReadBaseRepository<int, ScheduledEvent>
    {
        public IEnumerable<DateTime> GetAvailableAppointments(int doctorId, DateTime preferredDate);
    }
}
