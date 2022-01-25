using AutoMapper;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using HospitalApi.DTOs;

namespace HospitalApi.AutoMapperProfiles
{
    public class AutoMapperReport : Profile
    {
        public AutoMapperReport()
        {
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
            CreateMap<Report, ReportDTO>()
                .ForMember(o=> o.Doctor, opt=> opt.MapFrom(src=>src.ScheduledEvent.Doctor)).ReverseMap();
        }
       
    }
}

