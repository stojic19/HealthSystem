using AutoMapper;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using HospitalApi.DTOs;

namespace HospitalApi.AutoMapperProfiles
{
    public class PatientProfile : Profile
    {

        public PatientProfile() { 
            CreateMap<Measurements, MeasurementsDTO>().ReverseMap();
            CreateMap<CityDTO, City>();
            CreateMap<DoctorDTO, Doctor>();
            CreateMap<MedicationIngredientDTO, MedicationIngredient>();
            CreateMap<NewAllergyDTO, Allergy>();
            CreateMap<NewMedicalRecordDTO, MedicalRecord>();
            CreateMap<NewPatientDTO, Patient>();

            CreateMap<Doctor, SpecializedDoctorDTO>();
            CreateMap<MedicationIngredient, MedicationIngredientDTO>();
            CreateMap<City, CityDTO>();
            CreateMap<Allergy, AllergyDTO>();
            CreateMap<Doctor, DoctorDTO>();
            CreateMap<MedicalRecord, MedicalRecordDTO>();
            CreateMap<Patient, PatientDTO>();

            CreateMap<Patient, UserForBlockingDTO>();
        }
    }
}
