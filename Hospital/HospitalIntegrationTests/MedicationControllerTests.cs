using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalIntegrationTests
{
    public class MedicationControllerTests : BaseTest
    {
        public MedicationControllerTests(BaseFixture fixture) : base(fixture)
        {
            
        }

        [Fact]
        public async Task Add_medication_to_hospital_invalid_name_should_return_bad_request()
        {
            AddMedicationRequestDto newRequest = GetMedicationRequestWithInvalidMedicationName();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medication/AddMedicineQuantity", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Add_medication_to_hospital_invalid_quality_should_return_bad_request()
        {
            AddMedicationRequestDto newRequest = GetMedicationRequestWithInvalidQuantity();

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/Medication/AddMedicineQuantity", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        private AddMedicationRequestDto GetMedicationRequestWithInvalidMedicationName()
        {
            return new AddMedicationRequestDto()
            {
                MedicineName = "",
                Quantity = 10
            };
        }
        private AddMedicationRequestDto GetMedicationRequestWithInvalidQuantity()
        {
            return new AddMedicationRequestDto()
            {
                MedicineName = "Brufen",
                Quantity = 0
            };
        }
    }
}
