using Hospital.Schedule.Model;
using System.Collections.Generic;

namespace Hospital.SharedModel.Model
{
    public class Doctor : Staff
    {
        public int? SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public IEnumerable<ScheduledEvent> ScheduledEvents { get; set; }
        public IEnumerable<Vacation> Vacations { get; set; }

        public ICollection<OnCallDuty> OnCallDuties { get; set; }
        public int ShiftId { get; set; }
        public Shift Shift { get; set; }
    }
}
