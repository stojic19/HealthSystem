using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.EventStoring.Model.Wrappers
{
    public class SuccessSchedulingPerDayOfWeek
    {
        public List<int> Monday { get; set; }
        public List<int> Tuesday { get; set; }
        public List<int> Wednesday { get; set; }
        public List<int> Thursday { get; set; }
        public List<int> Friday { get; set; }
        public List<int> Saturday { get; set; }
        public List<int> Sunday { get; set; }
    }
}
