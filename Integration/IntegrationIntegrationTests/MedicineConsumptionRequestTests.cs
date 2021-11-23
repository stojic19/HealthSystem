using System;
using System.Net;
using System.Threading.Tasks;
using Integration.Model;
using IntegrationAPI.DTO;
using IntegrationIntegrationTests.Base;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace IntegrationIntegrationTests
{
    public class MedicineConsumptionRequestTests : BaseTest
    {
        public MedicineConsumptionRequestTests(BaseFixture fixture) : base(fixture)
        {
        }
        [Fact]
        public async Task Create_consumption_report_test()
        {
            var content = GetContent(new TimeRange {startDate = new DateTime(2021, 9, 1), endDate = new DateTime(2021, 10, 1)});

            var response = await Client.PostAsync("https://localhost:44302/api/Report/CreateConsumptionReport", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var medicineConsumptionReportDTO = JsonConvert.DeserializeObject<MedicineConsumptionReportDTO>(responseContent);
            medicineConsumptionReportDTO.MedicineConsumptions.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Send_consumption_report_test()
        {
            var content = GetContent(new TimeRange { startDate = new DateTime(2021, 9, 1), endDate = new DateTime(2021, 10, 1) });
            var response = await Client.PostAsync("https://localhost:44302/api/Report/CreateConsumptionReport", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var medicineConsumptionReportDTO = JsonConvert.DeserializeObject<MedicineConsumptionReportDTO>(responseContent);

            content = GetContent(medicineConsumptionReportDTO);
            response = await Client.PostAsync("https://localhost:44302/api/Report/SendConsumptionReport", content);
            responseContent = await response.Content.ReadAsStringAsync();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
