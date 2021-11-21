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
    public class MedicineSpecificationFileReadRepository : ReadBaseRepository<int, MedicineSpecificationFile>, IMedicineSpecificationFileReadRepository
    {
        public MedicineSpecificationFileReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
