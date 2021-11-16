using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class ComplaintResponse
    {
        public int Id { get; set; }

        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ComplaintId { get; set; }
        public Complaint Complaint { get; set; }
    }
}
