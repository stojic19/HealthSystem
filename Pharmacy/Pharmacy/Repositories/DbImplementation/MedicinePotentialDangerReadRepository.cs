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
    public class MedicinePotentialDangerReadRepository : ReadBaseRepository<int, MedicinePotentialDanger>, IMedicinePotentialDangerReadRepository
    {
        public MedicinePotentialDangerReadRepository(AppDbContext context) : base(context)
        {
        }

        public MedicinePotentialDanger GetMedicinePotentialDangerByName(string name)
        {
            return GetAll()
                .FirstOrDefault(x => x.Name.Equals(name));
        }
    }
}
