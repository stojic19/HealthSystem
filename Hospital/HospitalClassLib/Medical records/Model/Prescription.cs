using System;

namespace Hospital.Medical_records.Model
{
    public class Prescription
    {
        public int Id { get; set; }
        
        public Patient Patient { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime IssuedDate { get; set; }

        //Lek
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }

    }
}
