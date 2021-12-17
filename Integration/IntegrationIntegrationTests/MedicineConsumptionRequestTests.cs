using IntegrationIntegrationTests.Base;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using IntegrationAPI.Controllers.Medicine;
using IntegrationAPI.DTO.MedicineConsumption;
using IntegrationAPI.DTO.Shared;
using IntegrationAPI.HttpRequestSenders;
using Moq;
using RestSharp;
using Xunit;

namespace IntegrationIntegrationTests
{
    public class MedicineConsumptionRequestTests : BaseTest
    {
        public MedicineConsumptionRequestTests(BaseFixture fixture) : base(fixture)
        {
        }
        [Fact]
        public void Create_consumption_report_test()
        {
            string hospitalBaseUrl = "https://localhost:44303/";
            TimePeriodDTO timePeriodDto = new TimePeriodDTO
            { StartTime = new DateTime(2021, 9, 1), EndTime = new DateTime(2021, 10, 1)};
            var medicineConsumptionReportDto = GetMedicineConsumptionReportDtoTestData();
            var stubSender = MockSetup(medicineConsumptionReportDto, hospitalBaseUrl, timePeriodDto);
            ReportController controller = new ReportController(UoW, stubSender.Object);
            var result = controller.CreateConsumptionReport(timePeriodDto);
            result.MedicationExpenditureDTO.Count.ShouldBe(2);
            result.MedicationExpenditureDTO[0].Amount.ShouldBe(6);
        }

        private Mock<IHttpRequestSender> MockSetup(MedicineConsumptionReportDTO medicineConsumptionReportDto, string hospitalBaseUrl,
            TimePeriodDTO timePeriodDto)
        {
            var stubSender = new Mock<IHttpRequestSender>();
            RestResponse response = new RestResponse();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = JsonConvert.SerializeObject(medicineConsumptionReportDto);
            stubSender.Setup(m => m.Post(hospitalBaseUrl + "api/MedicationExpenditureReport/GetMedicationExpenditureReport",
                timePeriodDto)).Returns(response);
            return stubSender;
        }

        private MedicineConsumptionReportDTO GetMedicineConsumptionReportDtoTestData()
        {
            MedicineConsumptionReportDTO medicineConsumptionReportDto = new MedicineConsumptionReportDTO
            {
                startDate = new DateTime(2021, 9, 1),
                endDate = new DateTime(2021, 10, 1),
                createdDate = DateTime.Now,
                MedicationExpenditureDTO = new List<MedicationExpenditureDTO>()
            };
            medicineConsumptionReportDto.MedicationExpenditureDTO.Add(new MedicationExpenditureDTO
                {MedicineName = "TEST LEK 1", Amount = 5});
            medicineConsumptionReportDto.MedicationExpenditureDTO.Add(new MedicationExpenditureDTO
                {MedicineName = "TEST LEK 2", Amount = 6});
            return medicineConsumptionReportDto;
        }

        [Fact]
        public async Task Send_consumption_report_test()
        {
            MedicineConsumptionReportDTO medicineConsumptionReportDto = GetMedicineConsumptionReportDtoTestData();
            var content = GetContent(medicineConsumptionReportDto);
            var response = await Client.PostAsync(BaseUrl+ "api/Report/SendConsumptionReport", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
