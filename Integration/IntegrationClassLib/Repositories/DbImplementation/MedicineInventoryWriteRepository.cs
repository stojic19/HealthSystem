using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Repositories.DbImplementation
{
<<<<<<< HEAD
    public class MedicineInventoryWriteRepository : WriteBaseRepository<MedicineInventory>, IMedicineInventoryWriteRepository
    {
        public MedicineInventoryWriteRepository(AppDbContext context) : base(context)
=======
    class MedicineWriteRepository : WriteBaseRepository<Medicine>, IMedicineWriteRepository
    {
        public MedicineWriteRepository(AppDbContext context) : base(context)
>>>>>>> feature/integration-sftp-medicine-specification
        {

        }
    }
}
