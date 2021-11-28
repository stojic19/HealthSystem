using System.Collections.Generic;
using Hospital.Schedule.Model;
using Hospital.Shared_model.Model;
using Hospital.Shared_model.Model.Enumerations;

namespace Hospital.Rooms_and_equipment.Model
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
        public RoomType RoomType { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }

        public IEnumerable<RoomInventory> RoomInventories { get; set; }

        public IEnumerable<ScheduledEvent> ScheduledEvents { get; set; }
    }
}
