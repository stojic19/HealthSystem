using Hospital.SharedModel.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class MedicalRecordDTO
    {
        public MeasurementsDTO Measurements { get; set; }
        public BloodType BloodType { get; set; }
        public JobStatus JobStatus { get; set; }
        public IEnumerable<AllergyDTO> Allergies { get; set; }
        public DoctorDTO Doctor { get; set; }
    }
}
