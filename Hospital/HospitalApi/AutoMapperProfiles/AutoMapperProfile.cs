using AutoMapper;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Enumerations;
using HospitalApi.DTOs;
using System;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.Schedule.Model.Wrappers;

namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<RecommendedAppointmentDTO, ScheduledEvent>()
                .ForMember(toSchedule => toSchedule.EndDate, opt => opt.MapFrom(src => src.StartDate.AddHours(1)))
                .ForMember(toSchedule => toSchedule.IsCanceled, opt => opt.MapFrom(src => false))
                .ForMember(toSchedule => toSchedule.IsDone, opt => opt.MapFrom(src => false))
                .ForMember(toSchedule => toSchedule.ScheduledEventType,
                    opt => opt.MapFrom(src => ScheduledEventType.Appointment));
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

            CreateMap<Doctor, SpecializedDoctorDTO>();
            CreateMap<MedicationIngredient, MedicationIngredientDTO>();
            CreateMap<City, CityDTO>();
            CreateMap<Allergy, AllergyDTO>();
            CreateMap<Doctor, DoctorDTO>();
            CreateMap<MedicalRecord, MedicalRecordDTO>();
            CreateMap<Patient, PatientDTO>();
            CreateMap<EquipmentTransferEventDto, EquipmentTransferEvent>();
            CreateMap<DoctorAppointmentDTO, Doctor>().ReverseMap();
            CreateMap<AvailableAppointmentDTO, AvailableAppointment>().ReverseMap();
            CreateMap<Patient, UserForBlockingDTO>();
            CreateMap<RoomRenovationEventDto, RoomRenovationEvent>();
            CreateMap<NewManagerDTO, Manager>();
            CreateMap<ShiftDTO, Shift>();
            CreateMap<DoctorShiftDTO, Doctor>();
        }
    }
}
