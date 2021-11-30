using Integration.Database.EfStructures;
using Integration.Partnership.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Partnership.Repository.Implementation
{
    public class ReceiptWriteRepository : WriteBaseRepository<Receipt>, IReceiptWriteRepository
    {
        public ReceiptWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
