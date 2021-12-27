using Hospital.MedicalRecords.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using System;

namespace Hospital.Schedule.Model
{
    public class ScheduledEvent
    {
        public int Id { get; }
        public ScheduledEventType ScheduledEventType { get;  }
        public bool IsCanceled { get; }
        public bool IsDone { get; private set; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }    
        public DateTime CancellationDate { get; }
        public int PatientId { get; }
        public Patient Patient { get; private set; }
        public int DoctorId { get; }
        public Doctor Doctor { get; }
        public int? RoomId { get;  }
        public Room Room { get;  }

        public ScheduledEvent(int id, ScheduledEventType scheduledEventType, bool isCanceled, bool isDone, DateTime startDate, DateTime endDate, DateTime cancellationDate, int patientId, Patient patient, int doctorId, Doctor doctor)
        {
            Id = id;
            ScheduledEventType = scheduledEventType;
            IsCanceled = isCanceled;
            IsDone = isDone;
            StartDate = startDate;
            EndDate = endDate;
            CancellationDate = cancellationDate;
            PatientId = patientId;
            Patient = patient;
            DoctorId = doctorId;
            Doctor = doctor;
            Room = doctor.Room;
        }

        public ScheduledEvent()
        {
        }

        public void ScheduleEventForPatient(Patient patient)
        {
            Patient = patient;
        }
        public bool IsCanceledThisMonth()
        {
            return CancellationDate > DateTime.Now.AddDays(-30);
        }

        public void SetToDone()
        {
            IsDone = true;
        }

        public bool ShouldBeDone()
        {
            return !IsDone && !IsCanceled && DateTime.Compare(EndDate, DateTime.Now) < 0;
        }

        public bool IsUpcoming()
        {
            return !IsCanceled && !IsDone;
        }
        
        public bool IsUserCanceled()
        {
            return IsCanceled && !IsDone;
        }
    }
}
