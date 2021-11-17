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
    public class BenefitReadRepository : ReadBaseRepository<int, Benefit>, IBenefitReadRepository
    {
        public BenefitReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
