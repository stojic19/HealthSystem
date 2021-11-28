using Hospital.Rooms_and_equipment.Model;

namespace Hospital.Shared_model.Model
{
    public class Staff : User
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
