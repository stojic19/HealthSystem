using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ShiftReadRepository : ReadBaseRepository<int, Shift>, IShiftReadRepository
    {
        public ShiftReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
