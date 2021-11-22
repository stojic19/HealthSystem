using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class MedicineCombination
    {
        public int Id { get; set; }
        public int FirstMedicineId { get; set; }
        public Medicine FirstMedicine { get; set; }
        public int SecondMedicineId { get; set; } 
        public Medicine SecondMedicine { get; set; }
    }
}
