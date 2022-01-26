/*using Hospital.MedicalRecords.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
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

            //doctorsReport.NumOfAppointments.ShouldBe(0);
            doctorsReport.NumOfPatients.ShouldBe(0);
            doctorsReport.NumOfOnCallShifts.ShouldBe(0);
        }

        private void PrepareData()
        {
            ClearDbContext();
            Context.Patients.Add(new Patient()
            {
                Id = 1,
                UserName = "testPatient"
            });
            Context.SaveChanges();
            Doctor doctor = new Doctor()
            {
                Id = 2,
                UserName = "testDoctor",
                DoctorSchedule = new DoctorSchedule(3)
            };
            Context.Doctors.Add(doctor);
            Context.SaveChanges();

             Doctor secondDoctor = new Doctor()
             {
                 Id = 3,
                 UserName = "testDoctor2",
                 DoctorSchedule = new DoctorSchedule(5)
             };
             Context.Rooms.Add(new Room()
             {
                 Id = 4,
                 Name = "Test room 1"
             });
            ICollection<DoctorSchedule> DoctorsOnDuty = new List<DoctorSchedule>();
            DoctorsOnDuty.Add(doctor.DoctorSchedule);

            Context.OnCallDuties.Add(new OnCallDuty(12, 2, DoctorsOnDuty));
            Context.OnCallDuties.Add(new OnCallDuty(2, 1, DoctorsOnDuty));
            Context.SaveChanges();

            Context.ScheduledEvents.Add(new ScheduledEvent(1, ScheduledEventType.Appointment, false, false, new DateTime(2022, 12, 1, 10, 0, 0), new DateTime(2022, 12, 1, 10, 30, 0),
                new DateTime(2022, 02, 5, 3, 0, 0), 1, 2, null));
            Context.ScheduledEvents.Add(new ScheduledEvent(2, ScheduledEventType.Appointment, false, false, new DateTime(2022, 12, 3, 6, 0, 0), new DateTime(2022, 12, 3, 8, 0, 0),
                new DateTime(2022, 02, 5, 3, 0, 0), 1, 2, null));
            Context.ScheduledEvents.Add(new ScheduledEvent(3, ScheduledEventType.Appointment, false, false, new DateTime(2022, 02, 5, 3, 0, 0), new DateTime(2022, 02, 5, 3, 30, 0),
                new DateTime(2022, 02, 5, 3, 0, 0), 1, 2, null));
            Context.SaveChanges();
        }
    }
}*/
