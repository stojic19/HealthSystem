using Hospital.Schedule.Model;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using System.Collections.Generic;

namespace Hospital.MedicalRecords.Model
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        
        public double Height { get; set; }
        public double Weight { get; set; }
        
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public BloodType BloodType { get; set; }
        public JobStatus JobStatus { get; set; }    
        
        
        public IEnumerable<Referral> Referrals { get; set; }
        public IEnumerable<Allergy> Allergies { get; set; }
        public IEnumerable<Diagnose> Diagnoses { get; set; }    
        public IEnumerable<Prescription> Prescriptions { get; set; }
        public IEnumerable<HospitalTreatment> HospitalTreatments { get; set; }
        public IEnumerable<ScheduledEvent> ScheduledEvents { get; set; }
    }
}
