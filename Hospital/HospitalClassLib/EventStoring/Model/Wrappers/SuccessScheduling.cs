using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.EventStoring.Model.Wrappers
{
    public class SuccessScheduling
    {
        public DateTime StartScheduling { get; set; }
        public DateTime EndScheduling { get; set; }
        public string Username { get; set; }
    }
}
