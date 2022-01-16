using System.Collections.Generic;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model;

namespace Hospital.MedicalRecords.Model
{
    public class Patient : User
    {
        public int MedicalRecordId { get; private set; }
        public MedicalRecord MedicalRecord { get; private set; }
        public List<ScheduledEvent> ScheduledEvents { get; private set; } = new List<ScheduledEvent>();
        public Patient()
        {
        }

        public Patient(MedicalRecord medicalRecord)
        {
            MedicalRecordId = medicalRecord.Id;
            MedicalRecord = medicalRecord;
            ScheduledEvents = new List<ScheduledEvent>();
        }

        public Patient(int id,string username, MedicalRecord medicalRecord)
        {
            UserName = username;
            Id = id;
            MedicalRecordId = medicalRecord.Id;
            MedicalRecord = medicalRecord;
            ScheduledEvents = new List<ScheduledEvent>();
        }
        
        public ScheduledEvent ScheduleAppointment(ScheduledEvent newAppointment)
        {
            ScheduledEvents.Add(newAppointment);
            return newAppointment;
        }

        public void CancelAppointment(int eventId)
        {
            foreach (var se in ScheduledEvents.ToArray())
            {
                if (se.Id == eventId) se.SetToCanceled();
            }
        }
    }
}
