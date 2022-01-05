using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model;

namespace Hospital.MedicalRecords.Model
{
    public class Patient : User
    {
        public int MedicalRecordId { get; private set; }
        public MedicalRecord MedicalRecord { get; }
        public IEnumerable<ScheduledEvent> ScheduledEvents { get; }
        public Patient()
        {
        }

        public Patient(MedicalRecord medicalRecord)
        {
            MedicalRecordId = medicalRecord.Id;
            MedicalRecord = medicalRecord;
            ScheduledEvents = new List<ScheduledEvent>();
            //TODO: add validate method
        }

        public IEnumerable<ScheduledEvent> GetScheduledEvents()
        {
            return ScheduledEvents;
        }

        public void ScheduleAppointment(ScheduledEvent newAppointment)
        {
            ScheduledEvents.ToList().Add(newAppointment);
            //Validate();
        }

        public bool NameEquals(string userName)
        {
            return UserName.Equals(userName);
        }
    }
}
