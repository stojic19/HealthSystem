using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Model
{
    public class MedicineSpecificationFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Host { get; set; }
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
    }
}
