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
    public class EquipmentTransferEventWriteRepository : WriteBaseRepository<EquipmentTransferEvent>, IEquipmentTransferEventWriteRepository
    {
        public EquipmentTransferEventWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
