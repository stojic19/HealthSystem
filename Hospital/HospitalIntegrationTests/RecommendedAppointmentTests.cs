using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Newtonsoft.Json;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HospitalIntegrationTests
{
    public class RecommendedAppointmentTests : BaseTest
    {
        public RecommendedAppointmentTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Should_return_200_OK()
        {
            RegisterAndLogin("Patient");
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().First(d => d.UserName == "testDoctorUsername");
            var startDate = "2/2/2023";
            var endDate = "3/2/2023";
            var doctorId = doctor.Id;
            var isDoctorPriority = false;

            var response =
                await PatientClient.GetAsync(BaseUrl + "api/ScheduleAppointment/GetRecommendedAppointments?doctorId=" + doctorId + "&dateStart=" + startDate + "&dateEnd=" + endDate + "&isDoctorPriority=" + isDoctorPriority);
            var responseContent = await response.Content.ReadAsStringAsync();
            var availableAppointments =
                JsonConvert.DeserializeObject<IEnumerable<AvailableAppointmentDTO>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            availableAppointments.Count.ShouldNotBe(0);
         }

        [Fact]
        public async Task Should_return_available_appointments()
        {
            RegisterAndLogin("Patient");
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().First(d => d.UserName == "testDoctorUsername");
            var startDate = "2/2/2023";
            var endDate = "3/2/2023";
            var doctorId = doctor.Id;
            var isDoctorPriority = false;

            var response =
                await PatientClient.GetAsync(BaseUrl + "api/ScheduleAppointment/GetRecommendedAppointments?doctorId=" + doctorId + "&dateStart=" + startDate + "&dateEnd=" + endDate + "&isDoctorPriority=" + isDoctorPriority);
            var responseContent = await response.Content.ReadAsStringAsync();
            var availableAppointments =
                JsonConvert.DeserializeObject<IEnumerable<AvailableAppointmentDTO>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            availableAppointments.Count.ShouldNotBe(0);
            availableAppointments.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_return_available_appointments_for_doctor_priority()
        {
            RegisterAndLogin("Patient");
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().First(d => d.UserName == "testDoctorUsername");
            var dateStart = "2/2/2023";
            var dateEnd = "3/2/2023";
            var doctorId = doctor.Id;
            var isDoctorPriority = true;

            var response =
                await PatientClient.GetAsync(BaseUrl + "api/ScheduleAppointment/GetRecommendedAppointments?doctorId=" + doctorId + "&dateStart=" + dateStart + "&dateEnd=" + dateEnd + "&isDoctorPriority=" + isDoctorPriority);
            var responseContent = await response.Content.ReadAsStringAsync();
            var availableAppointments =
                JsonConvert.DeserializeObject<IEnumerable<AvailableAppointmentDTO>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            availableAppointments.Count.ShouldNotBe(0);
            availableAppointments.ShouldNotBeNull();
            availableAppointments[0].Doctor.Id.ShouldBe(doctorId);
        }

        [Fact]
        public async Task Should_return_available_appointments_for_date_priority()
        {
            RegisterAndLogin("Patient");
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().First(d => d.UserName == "testDoctorUsername");
            var dateStart = "12/14/2023";
            var dateEnd = "12/15/2023";
            var doctorId = doctor.Id;
            var isDoctorPriority = false;

            var response =
                await PatientClient.GetAsync(BaseUrl + "api/ScheduleAppointment/GetRecommendedAppointments?doctorId=" + doctorId + "&dateStart=" + dateStart + "&dateEnd=" + dateEnd + "&isDoctorPriority=" + isDoctorPriority);
            var responseContent = await response.Content.ReadAsStringAsync();
            var availableAppointments =
                JsonConvert.DeserializeObject<IEnumerable<AvailableAppointmentDTO>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            availableAppointments.Count.ShouldNotBe(0);
            availableAppointments.ShouldNotBeNull();
            availableAppointments[0].Doctor.Specialization.Equals(doctor.Specialization);
        }        
    }
}
