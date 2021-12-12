using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Model
{
    public class MedicationInventory
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        public int Quantity { get; set; }
    }
}
