using Hospital.MedicalRecords.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using System;

namespace Hospital.Schedule.Model
{
    public class ScheduledEvent
    {
        public int Id { get; private set; }
        public ScheduledEventType ScheduledEventType { get; private set; }
        public bool IsCanceled { get; private set; }
        public bool IsDone { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }    
        public DateTime CancellationDate { get; private set; }
        public int PatientId { get; private set;  }
        public Patient Patient { get; private set; }
        public int DoctorId { get; private set; }
        public Doctor Doctor { get; private set; }
        public int RoomId { get; private set; }
        public Room Room { get; private set; }

        public ScheduledEvent(ScheduledEventType scheduledEventType, bool isCanceled, bool isDone, DateTime startDate, DateTime endDate, DateTime cancellationDate, int patientId, int doctorId, Doctor doctor)
        {
            ScheduledEventType = scheduledEventType;
            IsCanceled = isCanceled;
            IsDone = isDone;
            StartDate = startDate;
            EndDate = endDate;
            CancellationDate = cancellationDate;
            PatientId = patientId;
            DoctorId = doctorId;
            Room = doctor.Room;
            RoomId = doctor.Room.Id;
            //TODO: add validate method
        }

        public ScheduledEvent()
        {
        }

        public void ScheduleEventForPatient(Patient patient)
        {
            PatientId = patient.Id;
            Patient = patient;
            //Validate();
        }

        public void ScheduleEventRoom(Room room)
        {
            RoomId = room.Id;
            Room = room;
            //Validate();
        }
        public bool IsCanceledThisMonth()
        {
            return CancellationDate > DateTime.Now.AddDays(-30);
        }

        public void SetToDone()
        {
            IsDone = true;
            //Validate();
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
