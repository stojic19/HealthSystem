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
    public class SubstanceReadRepository : ReadBaseRepository<int, Substance>, ISubstanceReadRepository
    {
        public SubstanceReadRepository(AppDbContext context) : base(context)
        {
        }

        public Substance GetSubstanceByName(string name)
        {
            return GetAll()
                .FirstOrDefault(x => x.Name == name);
        }
    }
}
