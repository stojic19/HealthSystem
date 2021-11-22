using Hospital.Model;
using Hospital.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public interface IEquipmentTransferEventWriteRepository : IWriteBaseRepository<EquipmentTransferEvent>
    {
    }
}
