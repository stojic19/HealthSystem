using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories.DbImplementation
{
    public class ComplaintResponseReadRepository : ReadBaseRepository<int, ComplaintResponse>, IComplaintResponseReadRepository
    {
        public ComplaintResponseReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
