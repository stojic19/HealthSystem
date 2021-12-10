using AutoMapper;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Enumerations;
using HospitalApi.DTOs;
using System;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using Hospital.RoomsAndEquipment.Model;

namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NewFeedbackDTO, Feedback>()
                  .ForMember(dto => dto.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                  .ForMember(dto => dto.FeedbackStatus, opt => opt.MapFrom(src => FeedbackStatus.Pending));
            CreateMap<Survey, SurveyStatisticDTO>();

            CreateMap<CityDTO, City>();

            CreateMap<DoctorDTO, Doctor>();
            CreateMap<MedicationIngredientDTO, MedicationIngredient>();
            CreateMap<NewAllergyDTO, Allergy>();
            CreateMap<NewMedicalRecordDTO, MedicalRecord>();
            CreateMap<NewPatientDTO, Patient>();
            CreateMap<EquipmentTransferEventDTO, EquipmentTransferEvent>();
        }
    }
}
