using System;
using Hospital.SharedModel.Model.Enumerations;

namespace HospitalApi.DTOs
{
    public class NewPatientDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public int CityId { get; set; }
        public CityDTO City { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        //public int MedicalRecordId { get; set; }
        public NewMedicalRecordDTO MedicalRecord { get; set; }
        public string PhotoEncoded { get; set; }
    }
}
