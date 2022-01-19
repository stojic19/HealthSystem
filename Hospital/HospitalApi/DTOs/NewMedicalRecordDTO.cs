using System.Collections.Generic;
using Hospital.SharedModel.Model.Enumerations;

namespace HospitalApi.DTOs
{
    public class NewMedicalRecordDTO
    {
        public int DoctorId { get; set; }
        public MeasurementsDTO Measurements { get; set; }
        public BloodType BloodType { get; set; }
        public JobStatus JobStatus { get; set; }
        public IEnumerable<NewAllergyDTO> Allergies { get; set; }
        //public int Id { get; set; }
  
    }
}