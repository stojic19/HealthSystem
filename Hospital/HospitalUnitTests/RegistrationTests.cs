using System.Linq;
using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository;
using HospitalUnitTests.Base;
using Shouldly;
using Xunit;

namespace HospitalUnitTests
{
    public class RegistrationTests : BaseTest
    {
        public RegistrationTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Should_return_doctors_with_ids_2_and_3()
        {
            ClearDbContext();
            Context.Shifts.Add(new Shift(1, "First Shift", 7, 15));
            Context.Doctors.Add(new Doctor(1, 1, null));
            Context.Doctors.Add(new Doctor(2, 1, null));
            Context.Doctors.Add(new Doctor(3, 1, null));
            Context.MedicalRecords.Add(new MedicalRecord(1, null, 0, 0, 1, null));
            Context.MedicalRecords.Add(new MedicalRecord(2, null, 0, 0, 1, null));
            Context.MedicalRecords.Add(new MedicalRecord(3, null, 0, 0, 1, null));
            Context.MedicalRecords.Add(new MedicalRecord(4, null, 0, 0, 2, null));
            Context.MedicalRecords.Add(new MedicalRecord(5, null, 0, 0, 1, null));
            Context.MedicalRecords.Add(new MedicalRecord(6, null, 0, 0, 2, null));
            Context.MedicalRecords.Add(new MedicalRecord(7, null, 0, 0, 3, null));
            Context.SaveChanges();

            var doctors = UoW.GetRepository<IDoctorReadRepository>().GetNonOverloadedDoctors().ToList();

            doctors.ShouldNotBeNull();
            doctors.Count.ShouldBe(2);
        }
    }
}
