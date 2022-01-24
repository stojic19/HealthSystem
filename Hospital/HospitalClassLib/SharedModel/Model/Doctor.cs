using Hospital.Schedule.Model;
using System.Collections.Generic;
using Hospital.RoomsAndEquipment.Model;

namespace Hospital.SharedModel.Model
{
    public class Doctor : User
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public Specialization Specialization { get; set; }
        public IEnumerable<ScheduledEvent> ScheduledEvents { get; set; }
        public int DoctorScheduleId { get; set; }
        public DoctorSchedule DoctorSchedule { get; set; }
        public int ShiftId { get; set; }
        public Shift Shift { get; set; }

        public Doctor()
        {
        }

        public Doctor(int id, int shiftId, Specialization specialization)
        {
            Id = id;
            ShiftId = shiftId;
            Specialization = specialization;
        }
        public Doctor(int id, int shiftId, Specialization specialization,Room room)
        {
            Id = id;
            ShiftId = shiftId;
            Specialization = specialization;
            Room = room;
        }
    }
}
