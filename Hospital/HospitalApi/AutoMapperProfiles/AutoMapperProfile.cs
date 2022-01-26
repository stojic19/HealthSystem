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
                .ForMember(toSchedule => toSchedule.EndDate, opt => opt.MapFrom(src => src.StartDate.AddMinutes(30)))
                .ForMember(toSchedule => toSchedule.IsCanceled, opt => opt.MapFrom(src => false))
                .ForMember(toSchedule => toSchedule.IsDone, opt => opt.MapFrom(src => false))
                .ForMember(toSchedule => toSchedule.ScheduledEventType,
                    opt => opt.MapFrom(src => ScheduledEventType.Appointment));
            CreateMap<Survey, SurveyStatisticDTO>();

            CreateMap<EquipmentTransferEventDto, EquipmentTransferEvent>();
            CreateMap<DoctorAppointmentDTO, Doctor>().ReverseMap();

            CreateMap<AvailableAppointmentDTO, AvailableAppointment>().ReverseMap();

            CreateMap<RoomRenovationEventDto, RoomRenovationEvent>();
            CreateMap<NewManagerDTO, Manager>();
            CreateMap<ShiftDTO, Shift>();
            CreateMap<DoctorShiftDTO, Doctor>();
           
            CreateMap<Medication, MedicationDTO>();
            CreateMap<Doctor, PrescriptionDoctorDTO>();
            CreateMap<Patient, PrescriptionPatientDTO>();
            CreateMap<Prescription, PrescriptionDTO>()
                .ForMember(dto => dto.PatientInfo, opt => opt.MapFrom(src => src.ScheduledEvent.Patient))
                    .ForMember(dto => dto.DoctorInfo, opt => opt.MapFrom(src => src.ScheduledEvent.Doctor));
        }
    }
}
