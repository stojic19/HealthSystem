using System;
using System.Collections.Generic;
using System.Linq;
using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Partnership.Service;
using Integration.Shared.Model;
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
            Context.Medicines.RemoveRange(Context.Medicines);
            Context.SaveChanges();
            MakeReceipts();
            Context.SaveChanges();
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
                {new TimeRange (new DateTime(2021, 9, 1), new DateTime(2021, 10, 1)), 3});
            retVal.Add(new object[]
                {new TimeRange (new DateTime(2020, 9, 1), new DateTime(2021, 11, 1)), 5});
            retVal.Add(new object[]
                {new TimeRange (new DateTime(2021, 10, 1),new DateTime(2021, 11, 1)), 1});
            retVal.Add(new object[]
                {new TimeRange (new DateTime(2021, 11, 1), new DateTime(2021, 12, 1)), 1});
            return retVal;
        }

        [Fact]
        public void Calculate_medicine_consumptions()
        {
            var receipts = UoW.GetRepository<IReceiptReadRepository>().GetAll().Include(x => x.Medicine);
            MedicineConsumptionCalculationMicroService medicineConsumptionConsumptionCalculationMicroService = new MedicineConsumptionCalculationMicroService();
            IEnumerable<MedicineConsumption> medicineConsumptions = medicineConsumptionConsumptionCalculationMicroService.CalculateMedicineConsumptions(receipts);
            medicineConsumptions.Count().ShouldBe(3);
        }
        [Theory]
        [MemberData(nameof(GetTimeRanges))]
        public void Create_medication_report(TimeRange timeRange, int shouldBe)
        {
            TimeRange september = new TimeRange
                (new DateTime(2021, 9, 1), new DateTime(2021, 10, 1));
            MedicineConsumptionMasterService service = new MedicineConsumptionMasterService(UoW);
            MedicineConsumptionReport report = service.CreateConsumptionReportInTimeRange(timeRange);
            report.MedicineConsumptions.Count().ShouldBe(shouldBe);
        }
        public static IEnumerable<object[]> GetTimeRanges()
        {
            TimeRange september = new TimeRange
                (new DateTime(2021, 9, 1), new DateTime(2021, 10, 1) );
            TimeRange november = new TimeRange
                (new DateTime(2021, 11, 1), new DateTime(2021, 12, 1));
            TimeRange december = new TimeRange
                (new DateTime(2021, 12, 1), new DateTime(2022, 1, 1));
            List<object[]> retVal = new List<object[]>();
            retVal.Add(new object[] {september, 3});
            retVal.Add(new object[] {november, 1});
            retVal.Add(new object[] { december, 2});
            return retVal;
        }
        private void MakeReceipts()
        {
            Medicine aspirin = new Medicine { Id = 1, Name = "Aspirin" };
            Medicine probiotik = new Medicine { Id = 2, Name = "Probiotik" };
            Medicine brufen = new Medicine { Id = 3, Name = "Brufen" };
            Context.Medicines.Add(aspirin);
            Context.Medicines.Add(probiotik);
            Context.Medicines.Add(brufen);
            Receipt receipt1 = new Receipt
            {
                Id = 1,
                ReceiptDate = new DateTime(2021, 9, 30),
                Medicine = brufen,
                AmountSpent = 4
            };
            Receipt receipt2 = new Receipt
            {
                Id = 2,
                ReceiptDate = new DateTime(2021, 5, 19),
                Medicine = probiotik,
                AmountSpent = 2
            };
            Receipt receipt3 = new Receipt
            {
                Id = 3,
                ReceiptDate = new DateTime(2021, 9, 19),
                Medicine = probiotik,
                AmountSpent = 4
            };
            Receipt receipt4 = new Receipt
            {
                Id = 4,
                ReceiptDate = new DateTime(2021, 10, 19),
                Medicine = brufen,
                AmountSpent = 1
            };
            Receipt receipt5 = new Receipt
            {
                Id = 5,
                ReceiptDate = new DateTime(2021, 9, 5),
                Medicine = aspirin,
                AmountSpent = 2
            };
            Receipt receipt6 = new Receipt
            {
                Id = 6,
                ReceiptDate = new DateTime(2021, 11, 5),
                Medicine = aspirin,
                AmountSpent = 5
            };
            Receipt receipt7 = new Receipt
            {
                Id = 7,
                ReceiptDate = new DateTime(2021, 12, 5),
                Medicine = brufen,
                AmountSpent = 8
            };
            Receipt receipt8 = new Receipt
            {
                Id = 8,
                ReceiptDate = new DateTime(2021, 12, 6),
                Medicine = aspirin,
                AmountSpent = 12
            };
            Context.Receipts.Add(receipt1);
            Context.Receipts.Add(receipt2);
            Context.Receipts.Add(receipt3);
            Context.Receipts.Add(receipt4);
            Context.Receipts.Add(receipt5);
            Context.Receipts.Add(receipt6);
            Context.Receipts.Add(receipt7);
            Context.Receipts.Add(receipt8);
        }

    }
}
