using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.MicroServices;
using Integration.Model;
using Integration.Repositories.DbImplementation;
using Shouldly;
using Xunit;

namespace IntegrationClassLibTests
{
    public class MedicationConsumptionTests
    {
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
