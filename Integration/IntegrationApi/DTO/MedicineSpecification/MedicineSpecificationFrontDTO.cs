using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO.MedicineSpecification
{
    public class MedicineSpecificationFrontDTO
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string PharmacyName { get; set; }
        public string MedicineName { get; set; }
        public DateTime ReceivedDate { get; set; }
    }
}
