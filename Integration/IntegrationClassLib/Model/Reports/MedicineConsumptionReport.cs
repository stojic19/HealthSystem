using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Model
{
    public class MedicineConsumptionReport
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime createdDate { get; set; }
        public IEnumerable<MedicineConsumption> MedicineConsumptions { get; set; }
    }
}
