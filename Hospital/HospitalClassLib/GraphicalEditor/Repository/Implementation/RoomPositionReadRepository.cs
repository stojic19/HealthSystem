using Hospital.Database.EfStructures;
using Hospital.GraphicalEditor.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.GraphicalEditor.Repository.Implementation
{
    public class RoomPositionReadRepository : ReadBaseRepository<int, RoomPosition>, IRoomPositionReadRepository
    {
        public RoomPositionReadRepository(AppDbContext context) : base(context)
        {
        }

        public RoomPosition GetByRoom(int? roomId)
        {
            var roomPositions = GetAll()
                                      .Where(roomPosition => roomPosition.RoomId == roomId);

            return roomPositions.FirstOrDefault();
        }
    }
}
