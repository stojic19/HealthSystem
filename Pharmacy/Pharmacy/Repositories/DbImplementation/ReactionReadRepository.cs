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
    public class ReactionReadRepository : ReadBaseRepository<int, Reaction>, IReactionReadRepository
    {
        public ReactionReadRepository(AppDbContext context) : base(context)
        {
        }

        public Reaction GetReactionByName(string name)
        {
            return GetAll()
                .FirstOrDefault(x => x.Name.Equals(name));
        }
    }
}
