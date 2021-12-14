using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class MedicineSpecificationFileDTO
    {
        public string FileName { get; set; }
        public string Host { get; set; }
        public string MedicineName { get; set; }
        public DateTime Date { get; set; }
    }
}
