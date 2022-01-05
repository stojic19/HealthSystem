using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Migrations;
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
        public TenderTests(BaseFixture fixture) : base(fixture)
        {
            ClearDbContext();
        }

        [Fact]
        public void Database_Test()
        {
            Tender tender = new Tender("Test_tender", new TimeRange(new DateTime(2020,1,1), new DateTime(2020, 2,2)));
            tender.AddMedicationRequest(new MedicationRequest("testlek1", 2));
            Pharmacy pharmacy = new Pharmacy();
            pharmacy.Name = "TestPharmacy";
            UoW.GetRepository<IPharmacyWriteRepository>().Add(pharmacy);
            TenderOffer tenderOffer = new TenderOffer(pharmacy, new Money(20, Currency.Din), DateTime.Now);
            tenderOffer.AddMedicationRequest(new MedicationRequest("testLek1", 1));
            tender.AddTenderOffer(tenderOffer);
            UoW.GetRepository<ITenderWriteRepository>().Add(tender);
            Tender fromRepo = UoW.GetRepository<ITenderReadRepository>().GetById(tender.Id);
            fromRepo.ShouldNotBeNull();
        }
    }
}
