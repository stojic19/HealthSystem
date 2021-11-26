using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class MedicineReportFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Host { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}
