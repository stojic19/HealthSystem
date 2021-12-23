using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Partnership.Repository;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Model;
using Integration.Shared.Repository;
using IntegrationAPI.DTO.Benefits;
using IntegrationIntegrationTests.Base;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Shouldly;
using Xunit;

namespace IntegrationIntegrationTests
{
    public class BenefitIntegrationTests : BaseTest
    {
        public BenefitIntegrationTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Receive_benefit_test()
        {
            DeleteTestData();
            GenerateTestData();
            BenefitCreateDTO dto = new BenefitCreateDTO
            {
                Title = "BENEFIT_TEST_TITLE",
                Description = "BENEFIT_TEST_DESCRIPTION",
                EndTime = DateTime.Today.AddDays(1),
                PharmacyName = "Benefit_Test_Pharmacy",
                StartTime = DateTime.Today.AddDays(-1)
            };
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "benefits", type: ExchangeType.Fanout);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));

                channel.BasicPublish(exchange: "benefits",
                    routingKey: "",
                    basicProperties: null,
                    body: body);
            }

            var benefit = UoW.GetRepository<IBenefitReadRepository>().GetAll()
                .FirstOrDefault(x => x.Title.Equals("BENEFIT_TEST_TITLE"));
            benefit.ShouldNotBeNull();
            benefit.Title.ShouldBe("BENEFIT_TEST_TITLE");
            DeleteTestData();
        }

        private void GenerateTestData()
        {
            Country country = new Country
            {
                Name = "Benefit_Test_Country"
            };
            UoW.GetRepository<ICountryWriteRepository>().Add(country);
            City city = new City
            {
                Name = "Benefit_Test_City",
                CountryId = country.Id
            };
            UoW.GetRepository<ICityWriteRepository>().Add(city);
            Pharmacy testPharmacy = new Pharmacy
            {
                Name = "Benefit_Test_Pharmacy",
                CityId = city.Id
            };
            UoW.GetRepository<IPharmacyWriteRepository>().Add(testPharmacy);
        }

        private void DeleteTestData()
        {
            var country = UoW.GetRepository<ICountryReadRepository>().GetByName("Benefit_Test_Country");
            if (country != null)
            {
                UoW.GetRepository<ICountryWriteRepository>().Delete(country);
            }
        }
    }
}
