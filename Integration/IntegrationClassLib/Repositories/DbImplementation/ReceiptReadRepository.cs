using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Integration.Repositories.DbImplementation
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
