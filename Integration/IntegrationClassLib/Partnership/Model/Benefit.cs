using System;
using Integration.Pharmacies.Model;

namespace Integration.Partnership.Model
{
    public class Benefit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public bool Published { get; set; }
        public bool Hidden { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
