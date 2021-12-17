using System.Linq;
using Hospital.MedicalRecords.Model;
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
            Context.Doctors.Add(new Doctor()
            {
                Id = 1
            });
            Context.Doctors.Add(new Doctor()
            {
                Id = 2
            });
            Context.Doctors.Add(new Doctor()
            {
                Id = 3
            });

            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 1,
                DoctorId = 1
            });
            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 2,
                DoctorId = 1
            });
            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 3,
                DoctorId = 1
            });
            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 4,
                DoctorId = 2
            });
            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 5,
                DoctorId = 1
            });
            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 6,
                DoctorId = 2
            });
            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 7,
                DoctorId = 3
            });
            Context.SaveChanges();

            var doctors = UoW.GetRepository<IDoctorReadRepository>().GetNonOverloadedDoctors().ToList();

            doctors.ShouldNotBeNull();
            doctors.Count().ShouldBe(2);
        }


    }
}
