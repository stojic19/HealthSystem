using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class ReceiptWriteRepository : WriteBaseRepository<Receipt>, IReceiptWriteRepository
    {
        public ReceiptWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
