using System;
using System.Collections.Generic;
using System.Linq;
using Integration.Model;
using Integration.Repositories;
using IntegrationUnitTests.Base;
using Shouldly;
using Xunit;

namespace IntegrationUnitTests
{
    public class MedicationConsumptionTests : BaseTest
    {
        public MedicationConsumptionTests(BaseFixture fixture) : base(fixture)
        {
            Medicine aspirin = new Medicine { Id = 1, Name = "Aspirin" };
            Medicine probiotik = new Medicine { Id = 2, Name = "Probiotik" };
            Medicine brufen = new Medicine { Id= 3, Name = "Brufen" };
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
            Context.Receipts.Add(receipt1);
            Context.Receipts.Add(receipt2);
            Context.Receipts.Add(receipt3);
            Context.Receipts.Add(receipt4);
            Context.Receipts.Add(receipt5);
        }
        [Fact]
        public void Get_receipts_in_time_range_september()
        {
            TimeRange timeRange = new TimeRange
            {
                startDate = new DateTime(2021, 9, 1),
                endDate = new DateTime(2021, 10, 1)
            };
            var receipts = UoW.GetRepository<IReceiptReadRepository>().GetReceiptLogsInTimeRange(timeRange);
            receipts.Count().ShouldBe(3);
        }
    }
}
