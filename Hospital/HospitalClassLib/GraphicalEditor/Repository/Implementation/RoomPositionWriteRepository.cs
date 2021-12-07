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
    class RoomPositionWriteRepository : WriteBaseRepository<RoomPosition>, IRoomPositionWriteRepository
    {
        public RoomPositionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
