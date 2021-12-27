using Hospital.Schedule.Model;
using System.Collections.Generic;
using Hospital.RoomsAndEquipment.Model;

namespace Hospital.SharedModel.Model
{
    public class Doctor : User
    {
        public Specialization Specialization { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
