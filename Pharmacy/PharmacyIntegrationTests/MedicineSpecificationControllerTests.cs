using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pharmacy.Model;
using Pharmacy.Repositories;
using PharmacyApi.DTO;
using PharmacyIntegrationTests.Base;
using Shouldly;
using Xunit;

namespace PharmacyIntegrationTests
{
    public class MedicineSpecificationControllerTests : BaseTest
    {
        public MedicineSpecificationControllerTests(BaseFixture fixture) : base(fixture)
        {
        }
        [Fact]
        public async Task Send_medicine_specification_file()
        {
            var hospital = new Hospital
            {
                ApiKey = Guid.NewGuid(),
                Name = "Test hospital",
                CityId = 1,
                StreetName = "test street",
                StreetNumber = "12",
                BaseUrl = "some url"
            };
            UoW.GetRepository<IHospitalWriteRepository>().Add(hospital);
            var content = new SpecificationDTO
            {
                ApiKey = hospital.ApiKey,
                MedicineName = "Aspirin"
            };
            var response = await Client.PostAsync(BaseUrl + "api/MedicineSpecification/MedicineSpecificationRequest", GetContent(content));
            UoW.GetRepository<IHospitalWriteRepository>().Delete(hospital);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
