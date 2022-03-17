using System;

namespace Hospital.MedicalRecords.Model
{
    public class MedicationExpenditureLog
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        public DateTime Date { get; set; }
        public int AmountSpent { get; set; }
    }
}
