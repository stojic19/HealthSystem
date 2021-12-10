using Hospital.Database.EfStructures;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.RoomsAndEquipment.Repository.Implementation
{
    public class RoomRenovationEventReadRepository : ReadBaseRepository<int, RoomRenovationEvent>, IRoomRenovationEventReadRepository
    {
        public RoomRenovationEventReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
