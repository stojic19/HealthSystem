using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class PrescriptionDTO
    {
        public PrescriptionPatientDTO PatientInfo { get; set; }
        public PrescriptionDoctorDTO DoctorInfo { get; set; }
        public MedicationDTO Medication { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime IssuedDate { get; set; }
    }
}
