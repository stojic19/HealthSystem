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
    public class MedicineSpecificationWriteRepository : WriteBaseRepository<MedicineSpecificationFile>, IMedicineSpecificationFileWriteRepository
    {
        public MedicineSpecificationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
