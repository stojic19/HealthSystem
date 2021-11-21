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
    public class PrecautionWriteRepository : WriteBaseRepository<Precaution>, IPrecautionWriteRepository
    {
        public PrecautionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
