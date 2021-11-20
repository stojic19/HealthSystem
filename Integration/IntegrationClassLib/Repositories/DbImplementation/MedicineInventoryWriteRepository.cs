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
    class MedicineWriteRepository : WriteBaseRepository<Medicine>, IMedicineWriteRepository
    {
        public MedicineWriteRepository(AppDbContext context) : base(context)
        {

        }
    }
}
