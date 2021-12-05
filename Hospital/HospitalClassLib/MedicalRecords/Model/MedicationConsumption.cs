using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Model
{
    public class MedicationConsumption
    {
        public Medication Medication { get; set; }
        public int Amount { get; set; }
    }
}
