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
   public class ReactionWriteRepository : WriteBaseRepository<Reaction>, IReactionWriteRepository
    {
        public ReactionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
