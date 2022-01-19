using Integration.Pharmacies.Model;
using Integration.Shared.Model;
using IntegrationAPI.DTO;
using IntegrationIntegrationTests.Base;
using Shouldly;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository;
using Xunit;

namespace IntegrationIntegrationTests
{
    public class MedicineControllerTests : BaseTest
    {
        public MedicineControllerTests(BaseFixture fixture) : base(fixture)
        {
            DeleteData();
            MakePharmacies();
        }

        [Fact]
        public async Task Check_medicine_availability_incorrect_pharmacyid_should_return_bad_request()
        {
            CreateMedicineRequestForPharmacyDto newRequest = GetRequestWithIncorrectPharmacyId();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/RequestMedicineInformation", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            DeleteData();
        }
        [Fact]
        public async Task Check_medicine_availability_incorrect_quantity_should_return_bad_request()
        {
            CreateMedicineRequestForPharmacyDto newRequest = GetRequestWithIncorrectQuantity();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/RequestMedicineInformation", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            DeleteData();
        }
        [Fact]
        public async Task Check_medicine_availability_no_answer_should_return_bad_request()
        {
            CreateMedicineRequestForPharmacyDto newRequest = GetRequestWithCorrectData();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/RequestMedicineInformation", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            DeleteData();
        }
        [Fact]
        public async Task Urgent_procurement_of_medicine_incorrect_pharmacyid_should_return_bad_request()
        {
            var newRequest = GetRequestWithIncorrectPharmacyId();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/UrgentProcurementOfMedicine", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            DeleteData();
        }
        [Fact]
        public async Task Urgent_procurement_of_medicine_incorrect_quantity_should_return_bad_request()
        {
            CreateMedicineRequestForPharmacyDto newRequest = GetRequestWithIncorrectQuantity();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/UrgentProcurementOfMedicine", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            DeleteData();
        }
        [Fact]
        public async Task Urgent_procurement_of_medicine_no_answer_should_return_bad_request()
        {
            CreateMedicineRequestForPharmacyDto newRequest = GetRequestWithCorrectData();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medicine/UrgentProcurementOfMedicine", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            DeleteData();
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
            var pharmacy = UoW.GetRepository<IPharmacyReadRepository>().GetByName("MEDICINE_CONTROLLER_TEST_PHARMACY").FirstOrDefault();
            return new CreateMedicineRequestForPharmacyDto()
            {
                PharmacyId = pharmacy.Id,
                ManufacturerName = "Hemofarm",
                MedicineName = "Brufen",
                Quantity = -1
            };
        }
        private CreateMedicineRequestForPharmacyDto GetRequestWithCorrectData()
        {
            var pharmacy = UoW.GetRepository<IPharmacyReadRepository>().GetByName("MEDICINE_CONTROLLER_TEST_PHARMACY").FirstOrDefault();
            return new CreateMedicineRequestForPharmacyDto()
            {
                PharmacyId = pharmacy.Id,
                ManufacturerName = "Hemofarm",
                MedicineName = "Brufen",
                Quantity = 10
            };
        }

        private void MakePharmacies()
        {
            UoW.GetRepository<IPharmacyWriteRepository>().Add(new Pharmacy()
            {
                City = new City()
                {
                    Country = new Country()
                    {
                        Name = "MEDICINE_CONTROLLER_TEST_COUNTRY",
                    },
                    Name = "MEDICINE_CONTROLLER_TEST_CITY"
                },
                ApiKey = new Guid(),
                BaseUrl = "baseUrl",
                Name = "MEDICINE_CONTROLLER_TEST_PHARMACY",
                StreetName = "Test Street Name",
                StreetNumber = "1"
            });
        }

        private void DeleteData()
        {
            var country = UoW.GetRepository<ICountryReadRepository>().GetByName("MEDICINE_CONTROLLER_TEST_COUNTRY");
            if (country != null)
            {
                UoW.GetRepository<ICountryWriteRepository>().Delete(country);
            }
        }
    }
}
