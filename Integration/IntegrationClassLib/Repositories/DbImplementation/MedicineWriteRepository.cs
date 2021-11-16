using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class MedicineWriteRepository : WriteBaseRepository<Medicine>, IMedicineWriteRepository
    {
        public MedicineWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
