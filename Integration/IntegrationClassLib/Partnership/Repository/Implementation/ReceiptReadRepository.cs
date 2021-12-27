using System.Collections.Generic;
using Integration.Database.EfStructures;
using Integration.Partnership.Model;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Integration.Partnership.Repository.Implementation
{
    public class ReceiptReadRepository : ReadBaseRepository<int, Receipt>, IReceiptReadRepository
    {
        public ReceiptReadRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Receipt> GetReceiptLogsInTimeRange(TimeRange timeRange)
        {
            List<Receipt> receiptLogsInTimeRange = new List<Receipt>();
            IEnumerable <Receipt> receipts = GetAll().Include(x => x.Medicine);
            foreach (var receiptLog in receipts)
                if (timeRange.StartDate < receiptLog.ReceiptDate && receiptLog.ReceiptDate < timeRange.EndDate)
                    receiptLogsInTimeRange.Add(receiptLog);
            return receiptLogsInTimeRange;
        }
    }
}
