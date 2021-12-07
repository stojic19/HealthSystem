using Hospital.RoomsAndEquipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.GraphicalEditor.Model
{
    public class RoomPosition
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public double DimensionX { get; set; }
        public double DimensionY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
