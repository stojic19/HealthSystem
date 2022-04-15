using System;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Model;
using Integration.Tendering.Model;
using Integration.Tendering.Repository;
using IntegrationUnitTests.Base;
using Shouldly;
using Xunit;

namespace IntegrationUnitTests
{
    public class TenderTests : BaseTest
    {
        private readonly IPharmacyWriteRepository _pharmacyWrite;
        private readonly ITenderWriteRepository _tenderWrite;
        public TenderTests(BaseFixture fixture) : base(fixture)
        {
            ClearDbContext();
            _pharmacyWrite = UoW.GetRepository<IPharmacyWriteRepository>();
            _tenderWrite = UoW.GetRepository<ITenderWriteRepository>();
        }

        [Fact]
        public void Database_test()
        {
            Tender tender = new("Test_tender", new TimeRange(new DateTime(2020,1,1), new DateTime(2020, 2,2)));
            tender.AddMedicationRequest(new MedicationRequest("testlek1", 2));

            Pharmacy pharmacy = new()
            {
                Name = "TestPharmacy"
            };
            _pharmacyWrite.Add(pharmacy);
            TenderOffer tenderOffer = new(pharmacy,
                                          new Money(20, Currency.Din),
                                          DateTime.Now);

            tenderOffer.AddMedicationRequest(new MedicationRequest("testLek1", 1));
            tender.AddTenderOffer(tenderOffer);

            _tenderWrite.Add(tender);
            Tender fromRepo = UoW.GetRepository<ITenderReadRepository>().GetById(tender.Id);
            fromRepo.ShouldNotBeNull();
        }

        [Fact]
        public void Closed_test()
        {
            Tender closedTender1 = new("closedTender1",
                new TimeRange(new DateTime(2021, 12, 14), DateTime.Now.AddDays(-1)));
            Tender closedTender2 = new("closedTender2",
                new TimeRange(new DateTime(2021, 12, 14), DateTime.Now.AddDays(1)));
            Tender closedTender3 = new("closedTender3",
                new TimeRange(new DateTime(2021, 12, 14), DateTime.Now.AddDays(-1)));
            closedTender3.CloseTender();
            Tender activeTender1 = new("activeTender1",
                new TimeRange(new DateTime(2021, 12, 14), DateTime.Now.AddDays(3)));
            closedTender2.CloseTender();
            Assert.False(closedTender1.IsActive());
            Assert.False(closedTender2.IsActive());
            Assert.False(closedTender3.IsActive());
            Assert.True(activeTender1.IsActive());
        }
    }
}
