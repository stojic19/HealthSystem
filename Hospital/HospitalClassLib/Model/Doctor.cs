using System.Collections.Generic;

namespace Hospital.Model
{
    public class Doctor : Staff
    {
        public int? SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public IEnumerable<ScheduledEvent> ScheduledEvents { get; set; }
    }
}
