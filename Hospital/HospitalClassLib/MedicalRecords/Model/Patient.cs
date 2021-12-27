using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model;

namespace Hospital.MedicalRecords.Model
{
    public class Patient : User
    {
        public int MedicalRecordId { get; }
        public MedicalRecord MedicalRecord { get; }
        public IEnumerable<ScheduledEvent> ScheduledEvents { get; }
        public Patient()
        {
        }

        public Patient(MedicalRecord medicalRecord)
        {
            MedicalRecord = medicalRecord;
            ScheduledEvents = new List<ScheduledEvent>();
        }

        public void ScheduleAppointment(ScheduledEvent newAppointment)
        {
            ScheduledEvents.ToList().Add(newAppointment);
        }

        public bool NameEquals(string userName)
        {
            return UserName.Equals(userName);
        }
    }
}
