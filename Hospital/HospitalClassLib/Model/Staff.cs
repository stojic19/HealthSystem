namespace Hospital.Model
{
    public class Staff : User
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
