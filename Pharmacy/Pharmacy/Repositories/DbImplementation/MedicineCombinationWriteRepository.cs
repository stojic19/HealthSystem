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
    public class MedicineCombinationWriteRepository : WriteBaseRepository<MedicineCombination>, IMedicineCombinationWriteRepository
    {
        public MedicineCombinationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}