using System;
using Integration.Pharmacies.Model;

namespace Integration.Partnership.Model
{
    public class MedicineSpecificationFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Host { get; set; }
        public string MedicineName { get; set; }
        public DateTime ReceivedDate { get; set; }
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
    }
}
