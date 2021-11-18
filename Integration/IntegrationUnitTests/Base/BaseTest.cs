using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.EfStructures;
using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;
using Xunit;

namespace IntegrationUnitTests.Base
{
    public class BaseTest : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;
        private static bool instanced = false;
        protected BaseTest(BaseFixture fixture)
        {
            _fixture = fixture;
            if (instanced) return;
            MakePharmacies();
            MakeReceipts();
            Context.SaveChanges();
            instanced = true;
        }

        private void MakePharmacies()
        {
            Pharmacy pharmacy1 = new Pharmacy
            {
                Id = 1,
                Name = "Apoteka1",
                City = new City
                {
                    Name = "Novi Sad",
                    Country = new Country { Name = "Srbija" }
                },
                StreetName = "Nova",
                StreetNumber = "29"
            };
            Pharmacy pharmacy2 = new Pharmacy
            {
                Id = 2,
                Name = "Apoteka",
                City = new City
                {
                    Name = "Novi Sad1",
                    Country = new Country { Name = "Srbija" }
                },
                StreetName = "Nova",
                StreetNumber = "29"
            };
            Pharmacy pharmacy3 = new Pharmacy
            {
                Id = 3,
                Name = "Apoteka",
                City = new City
                {
                    Name = "Novi Sad",
                    Country = new Country { Name = "Srbija1" }
                },
                StreetName = "Nova",
                StreetNumber = "29"
            };
            Pharmacy pharmacy4 = new Pharmacy
            {
                Id = 4,
                Name = "Apoteka",
                City = new City
                {
                    Name = "Novi Sad",
                    Country = new Country { Name = "Srbija" }
                },
                StreetName = "Nova1",
                StreetNumber = "29"
            };
            Pharmacy pharmacy5 = new Pharmacy
            {
                Id = 5,
                Name = "Apoteka",
                City = new City
                {
                    Name = "Novi Sad",
                    Country = new Country { Name = "Srbija" }
                },
                StreetName = "Nova",
                StreetNumber = "29b"
            };
            Context.Pharmacies.Add(pharmacy1);
            Context.Pharmacies.Add(pharmacy2);
            Context.Pharmacies.Add(pharmacy3);
            Context.Pharmacies.Add(pharmacy4);
            Context.Pharmacies.Add(pharmacy5);
        }
        private void MakeReceipts()
        {
            Medicine aspirin = new Medicine {Id = 1, Name = "Aspirin"};
            Medicine probiotik = new Medicine {Id = 2, Name = "Probiotik"};
            Medicine brufen = new Medicine {Id = 3, Name = "Brufen"};
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
            Context.Receipts.Add(receipt1);
            Context.Receipts.Add(receipt2);
            Context.Receipts.Add(receipt3);
            Context.Receipts.Add(receipt4);
            Context.Receipts.Add(receipt5);
            Context.Receipts.Add(receipt6);
        }

        protected IUnitOfWork UoW => _fixture.UoW;
        protected AppDbContext Context => _fixture.Context;
    }
}
