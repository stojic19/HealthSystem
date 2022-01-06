using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Model;
using Integration.Shared.Repository;
using IntegrationIntegrationTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Integration.Tendering.Model;
using Integration.Tendering.Model.RabbitMQMessages;
using Xunit;
using Integration.Tendering.Repository;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Newtonsoft.Json;
using Shouldly;

namespace IntegrationIntegrationTests
{
    public class TenderTests : BaseTest
    {
        public TenderTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Receive_tender_offer_test()
        {
            DeleteTestData();
            GenerateTestData();

            var tender = UoW.GetRepository<ITenderReadRepository>().GetAll()
                .Include(x => x.TenderOffers)
                .Include(x => x.MedicationRequests)
                .FirstOrDefault(x => x.Name.Equals("Tender_Test_Tender"));
            Pharmacy pharmacy = UoW.GetRepository<IPharmacyReadRepository>().GetByName("Tender_Test_Pharmacy").First();
            var requests = new List<MedicationRequestMessage>();
            requests.Add(new MedicationRequestMessage
            {
                MedicineName = "Tender_Test_Medicine",
                Quantity = 1
            });

            NewTenderOfferMessage dto = new NewTenderOfferMessage
            {
                Apikey = pharmacy.ApiKey,
                MedicationRequests = requests,
                CreatedDate = DateTime.Now,
                TenderCreatedDate = tender.CreatedTime,
                Cost = 1,
                Currency = 0
            };

            Thread.Sleep(10000); //maximum wait for creation of channel

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "new tender offer", type: ExchangeType.Direct);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));

                channel.BasicPublish(exchange: "new tender offer",
                    routingKey: pharmacy.ApiKey.ToString(),
                    basicProperties: null,
                    body: body);
            }

            Thread.Sleep(1000); //wait for database update

            tender = UoW.GetRepository<ITenderReadRepository>().GetAll()
                .Include(x => x.TenderOffers)
                .Include(x => x.MedicationRequests)
                .FirstOrDefault(x => x.Name.Equals("Tender_Test_Tender"));
            tender.ShouldNotBeNull();
            tender.Name.ShouldBe("Tender_Test_Tender");
            tender.TenderOffers.Count.ShouldBe(1);
            tender.TenderOffers.First().Pharmacy.Name.ShouldBe("Tender_Test_Pharmacy");

            DeleteTestData();
        }

        private void GenerateTestData()
        {
            Country country = new Country
            {
                Name = "Tender_Test_Country"
            };
            UoW.GetRepository<ICountryWriteRepository>().Add(country);
            City city = new City
            {
                Name = "Tender_Test_City",
                CountryId = country.Id
            };
            UoW.GetRepository<ICityWriteRepository>().Add(city);
            Pharmacy testPharmacy = new Pharmacy
            {
                ApiKey = Guid.NewGuid(),
                Name = "Tender_Test_Pharmacy",
                CityId = city.Id
            };
            UoW.GetRepository<IPharmacyWriteRepository>().Add(testPharmacy);

            Tender tender = new Tender("Tender_Test_Tender", new TimeRange(DateTime.Now, DateTime.Now.AddDays(2)));
            tender.AddMedicationRequest(new MedicationRequest("Tender_Test_Medicine", 1));
            UoW.GetRepository<ITenderWriteRepository>().Add(tender);
        }

        private void DeleteTestData()
        {
            var country = UoW.GetRepository<ICountryReadRepository>().GetByName("Tender_Test_Country");
            if (country != null)
            {
                UoW.GetRepository<ICountryWriteRepository>().Delete(country);
            }
            var tender = UoW.GetRepository<ITenderReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name.Equals("Tender_Test_Tender"));
            if (tender != null)
            {
                UoW.GetRepository<ITenderWriteRepository>().Delete(tender);
            }
        }
    }
}
