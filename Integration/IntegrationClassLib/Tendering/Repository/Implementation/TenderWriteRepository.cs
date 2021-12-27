using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Database.EfStructures;
using Integration.Shared.Repository.Base;
using Integration.Tendering.Model;

namespace Integration.Tendering.Repository.Implementation
{
    public class TenderWriteRepository : WriteBaseRepository<Tender>, ITenderWriteRepository
    {
        public TenderWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
