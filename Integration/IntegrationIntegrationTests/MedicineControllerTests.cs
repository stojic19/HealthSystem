using Integration.Pharmacies.Model;
using Integration.Shared.Model;
using IntegrationAPI.DTO;
using IntegrationIntegrationTests.Base;
using Shouldly;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationIntegrationTests
{
    public class MedicineControllerTests : BaseTest
    {
        public MedicineControllerTests(BaseFixture fixture) : base(fixture)
        {
            Context.Pharmacies.RemoveRange(Context.Pharmacies);
            Context.Countries.RemoveRange(Context.Countries);
            Context.Cities.RemoveRange(Context.Cities);
            Context.SaveChanges();
            MakePharmacies();
        }

        [Fact]
        public async Task Check_medicine_availability_incorrect_pharmacyid_should_return_bad_request()
        {
            CreateMedicineRequestForPharmacyDto newRequest = GetRequestWithIncorrectPharmacyId();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/RequestMedicineInformation", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Check_medicine_availability_incorrect_quantity_should_return_bad_request()
        {
            CreateMedicineRequestForPharmacyDto newRequest = GetRequestWithIncorrectQuantity();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/RequestMedicineInformation", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Check_medicine_availability_no_answer_should_return_bad_request()
        {
            CreateMedicineRequestForPharmacyDto newRequest = GetRequestWithCorrectData();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/RequestMedicineInformation", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Urgent_rocurement_of_medicine_incorrect_pharmacyid_should_return_bad_request()
        {
            var newRequest = GetRequestWithIncorrectPharmacyId();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/UrgentProcurementOfMedicine", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Urgent_rocurement_of_medicine_incorrect_quantity_should_return_bad_request()
        {
            CreateMedicineRequestForPharmacyDto newRequest = GetRequestWithIncorrectQuantity();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/UrgentProcurementOfMedicine", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Urgent_rocurement_of_medicine_no_answer_should_return_bad_request()
        {
            CreateMedicineRequestForPharmacyDto newRequest = GetRequestWithCorrectData();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/UrgentProcurementOfMedicine", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        private CreateMedicineRequestForPharmacyDto GetRequestWithIncorrectPharmacyId()
        {
            return new CreateMedicineRequestForPharmacyDto()
            {
                PharmacyId = -1,
                ManufacturerName = "Hemofarm",
                MedicineName = "Brufen",
                Quantity = 10
            };
        }
        private CreateMedicineRequestForPharmacyDto GetRequestWithIncorrectQuantity()
        {
            return new CreateMedicineRequestForPharmacyDto()
            {
                PharmacyId = 1,
                ManufacturerName = "Hemofarm",
                MedicineName = "Brufen",
                Quantity = -1
            };
        }
        private CreateMedicineRequestForPharmacyDto GetRequestWithCorrectData()
        {
            return new CreateMedicineRequestForPharmacyDto()
            {
                PharmacyId = 1,
                ManufacturerName = "Hemofarm",
                MedicineName = "Brufen",
                Quantity = 10
            };
        }

        private void MakePharmacies()
        {
            Context.Pharmacies.Add(new Pharmacy()
            {
                City = new City()
                {
                    Id = 1,
                    Country = new Country()
                    {
                        Name = "Test country",
                        Id = 1
                    },
                    Name = "Test city"
                },
                Id = 1,
                ApiKey = new Guid(),
                BaseUrl = "baseUrl",
                Name = "Test pharmacy",
                StreetName = "Test Street Name",
                StreetNumber = "1"
            });

            Context.SaveChanges();
        }
    }
}
