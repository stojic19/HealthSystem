using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.MasterServices;
using Integration.MicroServices;
using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using Integration.Repositories.DbImplementation;
using Moq;
using Shouldly;
using Xunit;

namespace IntegrationClassLibTests
{
    public class MedicationConsumptionTests
    {
        [Fact]
        public void Create_medication_report()
        {
            TimeRange september = TestData.getTimeRanges()[0];
            List<Receipt> allLogs = TestData.GetReceiptLogs();
            List<Receipt> retVal = new List<Receipt>();
            retVal.Add(allLogs[0]);
            retVal.Add(allLogs[2]);
            retVal.Add(allLogs[4]);
            var uow = new Mock<IUnitOfWork>();
            var repo = new Mock<IReceiptReadRepository>();
            uow.Setup(f => f.GetRepository<IReceiptReadRepository>()).Returns(repo.Object);
            repo.Setup(f => f.GetReceiptLogsInTimeRange(september)).Returns(retVal);

            MedicineConsumptionMasterService service = new MedicineConsumptionMasterService(uow.Object);
            MedicineConsumptionReport report = service.CreateConsumptionReportInTimeRange(september);

            report.MedicineConsumptions.Count().ShouldBe(3);
        }
        [Theory]
        [MemberData(nameof(ReceiptLogData))]
        public void Find_all_receipts_in_time_range(TimeRange timeRange, IEnumerable<Receipt> receiptLogs,
            IEnumerable<Receipt> shouldBe)
        {
            ReceiptMicroService receiptMicroService = new ReceiptMicroService();
            IEnumerable<Receipt> receiptsInTimeRange =
                receiptMicroService.GetReceiptLogsInTimeRange(timeRange, receiptLogs);
            receiptsInTimeRange.ShouldBe(shouldBe);
        }

        [Fact]
        public void Calculate_medicine_consumptions()
        {
            List<Receipt> allLogs = TestData.GetReceiptLogs();
            ReceiptMicroService receiptMicroService = new ReceiptMicroService();
            IEnumerable<MedicineConsumption> medicineConsumptions = receiptMicroService.CalculateMedicineConsumptions(allLogs);
            List<MedicineConsumption> shouldBe = new List<MedicineConsumption>();
            medicineConsumptions.Count().ShouldBe(3);
        }

        public static IEnumerable<object[]> ReceiptLogData()
        {
            List<TimeRange> timeRanges = TestData.getTimeRanges();
            List<Receipt> allLogs = TestData.GetReceiptLogs();
            List<Receipt> septemberShouldBe = new List<Receipt>();
            septemberShouldBe.Add(allLogs[0]);
            septemberShouldBe.Add(allLogs[2]);
            septemberShouldBe.Add(allLogs[4]);
            List<Receipt> octoberShouldBe = new List<Receipt>();
            octoberShouldBe.Add(allLogs[3]);
            List<Receipt> yearShouldBe = new List<Receipt>();
            yearShouldBe.Add(allLogs[0]);
            yearShouldBe.Add(allLogs[1]);
            yearShouldBe.Add(allLogs[2]);
            yearShouldBe.Add(allLogs[4]);
            List<object[]> retVal = new List<object[]>();
            retVal.Add(new object[]{ timeRanges[0], allLogs, septemberShouldBe});
            retVal.Add(new object[]{ timeRanges[1], allLogs, octoberShouldBe });
            retVal.Add(new object[]{ timeRanges[2], allLogs, yearShouldBe});
            return retVal;
        }
    }
}
