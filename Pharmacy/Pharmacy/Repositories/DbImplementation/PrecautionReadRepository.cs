using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Repositories.DbImplementation
{
    public class PrecautionReadRepository : ReadBaseRepository<int, Precaution>, IPrecautionReadRepository
    {
        public PrecautionReadRepository(AppDbContext context) : base(context)
        {
        }

        public Precaution GetPrecautionByName(string name)
        {
            return GetAll()
                .FirstOrDefault(x => x.Name.Equals(name));
        }
    }
}
