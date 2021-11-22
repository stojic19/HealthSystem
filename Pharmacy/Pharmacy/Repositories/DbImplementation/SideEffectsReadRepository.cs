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
    public class SideEffectsReadRepository : ReadBaseRepository<int, SideEffect>, ISideEffectReadRepository
    {
        public SideEffectsReadRepository(AppDbContext context) : base(context)
        {
        }

        public SideEffect GetSideEffectByName(string name)
        {
            return GetAll()
                .FirstOrDefault(x => x.Name.Equals(name));
        }
    }
}
