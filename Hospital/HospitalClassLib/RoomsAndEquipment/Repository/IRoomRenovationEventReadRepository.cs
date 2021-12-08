using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.RoomsAndEquipment.Repository
{
    public interface IRoomRenovationEventReadRepository : IReadBaseRepository<int, RoomRenovationEvent>
    {
    }
}
