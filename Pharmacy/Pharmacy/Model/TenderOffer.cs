using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class TenderOffer
    {
        public int Id { get; set; }
        public List<MedicationRequest> MedicationRequests { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsWinning { get; set; }
        public int TenderId { get; set; }
        public Tender Tender { get; set; }
        public Money Cost { get; set; }
    }
}
