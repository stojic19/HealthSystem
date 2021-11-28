using System.Collections.Generic;
using Hospital.Schedule.Model;

namespace Hospital.Shared_model.Model
{
    public class Doctor : Staff
    {
        public int? SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public IEnumerable<ScheduledEvent> ScheduledEvents { get; set; }
    }
}
