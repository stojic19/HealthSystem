using System.Collections.Generic;
using Integration.Partnership.Model;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Partnership.Repository
{
    public interface IReceiptReadRepository : IReadBaseRepository<int, Receipt>
    {
        public IEnumerable<Receipt> GetReceiptLogsInTimeRange(TimeRange timeRange);
    }
}
