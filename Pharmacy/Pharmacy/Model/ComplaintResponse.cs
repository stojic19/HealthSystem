using Pharmacy.Model;
using System;

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
