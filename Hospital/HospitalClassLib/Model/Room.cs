using System.Collections.Generic;

namespace Hospital.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DimensionX { get; set; }
        public double DimensionY { get; set; }
        public int FloorNumber { get; set; }
        public string BuildingName { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; }

        public IEnumerable<RoomInventory> RoomInventories { get; set; }

        public IEnumerable<ScheduledEvent> ScheduledEvents { get; set; }
    }
}
