using System;
using System.Collections.Generic;
using System.Linq;
using Integration.MasterServices;
using Integration.MicroServices;
using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using IntegrationUnitTests.Base;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace IntegrationUnitTests
{
    public class MedicationConsumptionTests : BaseTest
    {
        
        public MedicationConsumptionTests(BaseFixture fixture) : base(fixture)
        {
            
        }

        [Theory]
        [MemberData(nameof(GetReceiptsData))]
        public void Get_receipts_in_time_range(TimeRange timeRange, int shouldBe)
        {
            var receipts = UoW.GetRepository<IReceiptReadRepository>().GetReceiptLogsInTimeRange(timeRange);
            receipts.Count().ShouldBe(shouldBe);
        }

        public static IEnumerable<object[]> GetReceiptsData()
        {
            List<object[]> retVal = new List<object[]>();
            retVal.Add(new object[]
                {new TimeRange {startDate = new DateTime(2021, 9, 1), endDate = new DateTime(2021, 10, 1)}, 3});
            retVal.Add(new object[]
                {new TimeRange {startDate = new DateTime(2020, 9, 1), endDate = new DateTime(2021, 11, 1)}, 5});
            retVal.Add(new object[]
                {new TimeRange {startDate = new DateTime(2021, 10, 1), endDate = new DateTime(2021, 11, 1)}, 1});
            retVal.Add(new object[]
                {new TimeRange {startDate = new DateTime(2021, 11, 1), endDate = new DateTime(2021, 12, 1)}, 1});
            return retVal;
        }

        [Fact]
        public void Calculate_medicine_consumptions()
        {
            var receipts = UoW.GetRepository<IReceiptReadRepository>().GetAll().Include(x => x.Medicine);
            ReceiptMicroService receiptMicroService = new ReceiptMicroService();
            IEnumerable<MedicineConsumption> medicineConsumptions = receiptMicroService.CalculateMedicineConsumptions(receipts);
            medicineConsumptions.Count().ShouldBe(3);
        }
        [Fact]
        public void Create_medication_report_september()
        {
            TimeRange september = new TimeRange
                {startDate = new DateTime(2021, 9, 1), endDate = new DateTime(2021, 10, 1)};
            MedicineConsumptionMasterService service = new MedicineConsumptionMasterService(UoW);
            MedicineConsumptionReport report = service.CreateConsumptionReportInTimeRange(september);
            report.MedicineConsumptions.Count().ShouldBe(3);
        }
        [Fact]
        public void Create_medication_report_november()
        {
            TimeRange september = new TimeRange
                { startDate = new DateTime(2021, 11, 1), endDate = new DateTime(2021, 12, 1) };
            MedicineConsumptionMasterService service = new MedicineConsumptionMasterService(UoW);
            MedicineConsumptionReport report = service.CreateConsumptionReportInTimeRange(september);
            report.MedicineConsumptions.Count().ShouldBe(1);
        }

    }
}
