using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace HospitalIntegrationTests
{
    public class RegistrationTests : BaseTest
    {
        public RegistrationTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Register_should_return_200()
        {
            ClearUser();

            var newPatient = InsertPatient();

            var content = GetContent(newPatient);

            var response = await PatientClient.PostAsync(BaseUrl + "api/Registration/Register", content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var foundRegisteredUser = UoW.GetRepository<IPatientReadRepository>()
                .GetAll().FirstOrDefault(x => x.UserName.ToUpper().Equals(newPatient.UserName.ToUpper()));

            ClearAllTestData();

            foundRegisteredUser.ShouldNotBeNull();
            foundRegisteredUser.UserName.ShouldBe("testUserName1");
        }

        private void ClearUser()
        {
            var user = UoW.GetRepository<IPatientReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.UserName == "testUserName1");
            if (user == null) return;
            {
                var medicalRecord = UoW.GetRepository<IMedicalRecordReadRepository>()
                    .GetAll()
                    .FirstOrDefault(x => x.Id == user.MedicalRecordId);

                if (medicalRecord != null) UoW.GetRepository<IMedicalRecordWriteRepository>().Delete(medicalRecord);
            }
        }

        private void ClearAllTestData()
        {
            var user = UoW.GetRepository<IPatientReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.UserName == "testUserName1");

            if (user == null) return;
            {
                var medicalRecord = UoW.GetRepository<IMedicalRecordReadRepository>()
                    .GetAll().Include(mr => mr.Doctor)
                    .FirstOrDefault(x => x.Id == user.MedicalRecordId);

                UoW.GetRepository<IMedicalRecordWriteRepository>().Delete(medicalRecord);
            }

            var doctor = UoW.GetRepository<IDoctorReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.UserName == "Test doctor");

            if (doctor != null)
            {
                UoW.GetRepository<IDoctorWriteRepository>().Delete(doctor);
            }

            var room = UoW.GetRepository<IRoomReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "test room");

            if (room != null)
            {
                UoW.GetRepository<IRoomWriteRepository>().Delete(room);
            }
        }

        private NewPatientDTO InsertPatient()
        {
            var doctor = UoW
                    .GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization).Include(d => d.Room)
                    .FirstOrDefault(d => d.Specialization.Name.ToLower().Equals("general practice"));

            if (doctor == null)
            {
                var room = UoW.GetRepository<IRoomReadRepository>()
                    .GetAll()
                    .FirstOrDefault(x => x.RoomType == RoomType.AppointmentRoom);

                if (room == null)
                {
                    room = new Room()
                    {
                        Name = "test room",
                        RoomType = RoomType.AppointmentRoom
                    };
                    UoW.GetRepository<IRoomWriteRepository>().Add(room);
                }
                
                var shift = UoW.GetRepository<IShiftReadRepository>()
                    .GetAll()
                    .FirstOrDefault(x => x.Name.ToLower().Equals("test shiift"));

                if (shift == null)
                {
                    shift = new Shift()
                    {
                        Name = "test shiift",
                        From = 7,
                        To = 15
                    };
                    UoW.GetRepository<IShiftWriteRepository>().Add(shift);
                }

                doctor = new Doctor()
                {
                    UserName = "Test doctor",
                    RoomId = room.Id,
                    Specialization = new Specialization("General Practice", ""),
                    ShiftId = shift.Id
                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            return new NewPatientDTO()
            {
                UserName = "testUserName1",
                Email = "test1email@gmail.com",
                Password = "Test Passw0rd",
                MedicalRecord = new NewMedicalRecordDTO()
                {
                    DoctorId = doctor.Id
                }
            };

        }
    }
}