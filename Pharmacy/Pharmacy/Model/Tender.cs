using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class Tender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public TimeRange ActiveRange { get; set; }
        public List<MedicationRequest> MedicationRequests { get; set; }
    }
}
