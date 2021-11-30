using Hospital.RoomsAndEquipment.Model;

namespace Hospital.SharedModel.Model
{
    public class Staff : User
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
