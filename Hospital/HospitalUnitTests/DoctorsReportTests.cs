using Hospital.MedicalRecords.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository;
using HospitalUnitTests.Base;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalUnitTests
{
    public class DoctorsReportTests : BaseTest
    {
        public DoctorsReportTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Report_should_find_two_appointments()
        {
            PrepareData();

            TimePeriod dateRange = new TimePeriod(new DateTime(2022, 12, 1, 0, 0, 0),
                new DateTime(2022, 12, 31, 0, 0, 0));
            GeneratingDoctorsReportService reportService = new GeneratingDoctorsReportService(UoW);
            DoctorsScheduleReport doctorsReport = reportService.GetReportInformation(dateRange, 2);

            doctorsReport.NumOfAppointments.ShouldBe(2);
            doctorsReport.NumOfPatients.ShouldBe(1);
            doctorsReport.NumOfOnCallShifts.ShouldBe(1);
        }

        [Fact]
        public void Report_should_generate_no_appointments()
        {
            PrepareData();

            TimePeriod dateRange = new TimePeriod(new DateTime(2022, 01, 1, 0, 0, 0),
                new DateTime(2022, 01, 31, 0, 0, 0));
            GeneratingDoctorsReportService reportService = new GeneratingDoctorsReportService(UoW);
            DoctorsScheduleReport doctorsReport = reportService.GetReportInformation(dateRange, 2);

            doctorsReport.NumOfAppointments.ShouldBe(0);
            doctorsReport.NumOfPatients.ShouldBe(0);
            doctorsReport.NumOfOnCallShifts.ShouldBe(0);
        }

        private void PrepareData()
        {
            ClearDbContext();
            Doctor doctor = new Doctor()
            {
                Id = 2,
                UserName = "testDoctor",
            };
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room 1"
            });
            Context.Patients.Add(new Patient()
            {
                Id = 1,
                UserName = "testPatient"
            });
            Context.Doctors.Add(doctor);
            ICollection<Doctor> DoctorsOnDuty = new List<Doctor>();
            DoctorsOnDuty.Add(doctor);

            Context.OnCallDuties.Add(new OnCallDuty(12, 2, DoctorsOnDuty));

            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = new DateTime(2022, 12, 1, 10, 0, 0),
                EndDate = new DateTime(2022, 12, 1, 10, 30, 0),
                PatientId = 1,
                DoctorId = 2,
                RoomId = 1,
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 2,
                StartDate = new DateTime(2022, 12, 3, 6, 0, 0),
                EndDate = new DateTime(2022, 12, 3, 8, 0, 0),
                PatientId = 1,
                DoctorId = 2,
                RoomId = 1,
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 3,
                StartDate = new DateTime(2022, 02, 5, 3, 0, 0),
                EndDate = new DateTime(2022, 02, 5, 3, 30, 0),
                PatientId = 1,
                DoctorId = 2,
                RoomId = 1,
            });
            Context.SaveChanges();
        }
    }
}
