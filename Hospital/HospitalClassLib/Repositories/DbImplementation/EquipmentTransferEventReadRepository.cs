using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.DbImplementation
{
    public class EquipmentTransferEventReadRepository : ReadBaseRepository<int, EquipmentTransferEvent>, IEquipmentTransferEventReadRepository
    {
        public EquipmentTransferEventReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
