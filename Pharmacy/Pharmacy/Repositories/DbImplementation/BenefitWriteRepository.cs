using Pharmacy.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Model;
using Pharmacy.EfStructures;

namespace Pharmacy.Repositories.DbImplementation
{
    class BenefitWriteRepository : WriteBaseRepository<Benefit>, IBenefitWriteRepository
    {
        public BenefitWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
