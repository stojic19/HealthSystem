using System;

namespace Integration.Model
{
    public class Complaint
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public ComplaintResponse ComplaintResponse { get; set; }

        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}
