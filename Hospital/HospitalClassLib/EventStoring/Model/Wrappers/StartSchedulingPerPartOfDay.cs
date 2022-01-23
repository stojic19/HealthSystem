using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.EventStoring.Model.Wrappers
{
    public class StartSchedulingPerPartOfDay
    {
        public int From0To8 { get; set; }
        public int From8To12 { get; set; }
        public int From12To16 { get; set; }
        public int From16To20 { get; set; }
        public int From20To00 { get; set; }
    }
}
