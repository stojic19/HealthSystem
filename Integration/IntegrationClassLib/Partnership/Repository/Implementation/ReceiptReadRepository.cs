using Integration.Database.EfStructures;
using Integration.Partnership.Model;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
                if (timeRange.startDate < receiptLog.ReceiptDate && receiptLog.ReceiptDate < timeRange.endDate)
                    receiptLogsInTimeRange.Add(receiptLog);
            return receiptLogsInTimeRange;
        }
    }
}
