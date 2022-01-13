using AutoMapper;
using AutoMapper.Configuration;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SeleniumTests
{
    [Collection("Sequence")]
    public class BlockMaliciousPatientsTest : BaseTest, IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly Pages.BlockMaliciousPatientsPage _blockMaliciousPatientsPage;
        private LoginPage loginPage;
        private int patientCount = 0;

        public BlockMaliciousPatientsTest(BaseFixture fixture) : base(fixture)
        {
            var options = new ChromeOptions();
            options.AddArguments("start-maximized");
            options.AddArguments("disable-infobars");
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-notifications");
            _driver = new ChromeDriver(options);
            loginPage = new LoginPage(_driver);
            loginPage.Navigate();
            loginPage.EnsureLoginFormForAdminIsDisplayed();
            _blockMaliciousPatientsPage = new Pages.BlockMaliciousPatientsPage(_driver);
        }

        [Fact]
        public void BlockPatient()
        {
            RegisterUser("Manager");
            ArrangeDatabase();
            InsertCredentials();
            loginPage.EnsureAdminIsLoggedIn();
            _blockMaliciousPatientsPage.Navigate();
            _blockMaliciousPatientsPage.EnsurePageIsDisplayed();
            patientCount = _blockMaliciousPatientsPage.PatientCount();
            _blockMaliciousPatientsPage.BlockPatient();
            Assert.True(_blockMaliciousPatientsPage.IsSnackBarDisplayed());
            Assert.Equal(_driver.Url, _blockMaliciousPatientsPage.URI);
            //Assert.Equal(patientCount - 1, _.PatientCount());
            //ClearDatabase();
            DeleteDataFromDataBase();
        }
        private void InsertCredentials()
        {
            loginPage.InsertUsername("testLogInManager");
            loginPage.InsertPassword("111111aA");
            loginPage.Submit();
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
        private void ArrangeDatabase()
        {
            RegisterUser("Patient");
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll().Include(p => p.MedicalRecord).ThenInclude(mr => mr.Doctor)
                .FirstOrDefault(p => p.UserName == "testPatientUsername");

                var date = new DateTime();
                var scheduledEvent = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                    .FirstOrDefault(x => x.StartDate == date && x.Patient.Id == patient.Id && x.Doctor.Id == patient.MedicalRecord.DoctorId);
                if (scheduledEvent != null) return;
                scheduledEvent = new ScheduledEvent()
                {
                    StartDate = DateTime.Now.AddDays(-100),
                    EndDate = DateTime.Now.AddDays(-100),
                    CancellationDate = DateTime.Now.AddDays(-110),
                    Doctor = patient.MedicalRecord.Doctor,
                    IsCanceled = true,
                    IsDone = false,
                    Patient = patient,
                    Room = patient.MedicalRecord.Doctor.Room,
                    ScheduledEventType = ScheduledEventType.Appointment
                };
                UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);

                var scheduledEvent2 = new ScheduledEvent()
                {
                    StartDate = DateTime.Now.AddDays(-22),
                    EndDate = DateTime.Now.AddDays(-22),
                    CancellationDate = DateTime.Now.AddDays(-27),
                    Doctor = patient.MedicalRecord.Doctor,
                    IsCanceled = true,
                    IsDone = false,
                    Patient = patient,
                    Room = patient.MedicalRecord.Doctor.Room,
                    ScheduledEventType = ScheduledEventType.Appointment
                };
                UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent2);
                var scheduledEvent3 = new ScheduledEvent()
                {
                    StartDate = DateTime.Now.AddDays(-4),
                    EndDate = DateTime.Now.AddDays(-4),
                    CancellationDate = DateTime.Now.AddDays(-14),
                    Doctor = patient.MedicalRecord.Doctor,
                    IsCanceled = true,
                    IsDone = false,
                    Patient = patient,
                    Room = patient.MedicalRecord.Doctor.Room,
                    ScheduledEventType = ScheduledEventType.Appointment
                };
                UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent3);
                var scheduledEvent4 = new ScheduledEvent()
                {
                    StartDate = DateTime.Now.AddDays(7),
                    EndDate = DateTime.Now.AddDays(7),
                    CancellationDate = DateTime.Now.AddDays(-1),
                    Doctor = patient.MedicalRecord.Doctor,
                    IsCanceled = true,
                    IsDone = false,
                    Patient = patient,
                    Room = patient.MedicalRecord.Doctor.Room,
                    ScheduledEventType = ScheduledEventType.Appointment
                };
                UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent4);
        }
        

        private void ClearDatabase()
        {
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
               .FirstOrDefault(x => x.UserName == "testUsername");
            if (patient == null) return;

            {
                var medicalRecord = UoW.GetRepository<IMedicalRecordReadRepository>()
                    .GetAll().Include(mr => mr.Doctor)
                    .FirstOrDefault(x => x.Id == patient.MedicalRecordId);

                UoW.GetRepository<IMedicalRecordWriteRepository>().Delete(medicalRecord);
            }
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "TestDoctorUsername");
            if (doctor != null)
            {
                UoW.GetRepository<IDoctorWriteRepository>().Delete(doctor);
            }
            var specialization = UoW.GetRepository<ISpecializationReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name == "TestSpecialization");
            if (specialization != null)
            {
                UoW.GetRepository<ISpecializationWriteRepository>().Delete(specialization);
            }
            var city = UoW.GetRepository<ICityReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "TestCity");

            if (city != null)
            {
                UoW.GetRepository<ICityWriteRepository>().Delete(city);
            }

            var country = UoW.GetRepository<ICountryReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "TestCountry");

            if (country != null)
            {
                UoW.GetRepository<ICountryWriteRepository>().Delete(country);
            }

            var room = UoW.GetRepository<IRoomReadRepository>()
                    .GetAll().ToList()
                    .FirstOrDefault(x => x.Name == "TestRoom");

            if (room != null)
            {
                UoW.GetRepository<IRoomWriteRepository>().Delete(room);
            }
            var shift = UoW.GetRepository<IShiftReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "prva");

            if (shift != null)
            {
                UoW.GetRepository<IShiftWriteRepository>().Delete(shift);
            }
        }
    }
}
