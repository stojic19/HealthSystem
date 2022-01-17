using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Model;
using Integration.Shared.Repository;
using IntegrationApi.Controllers.Advertisement;
using IntegrationApi.DTO.Advertisement;
using IntegrationAPI.HttpRequestSenders;
using IntegrationIntegrationTests.Base;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using Shouldly;
using Xunit;

namespace IntegrationIntegrationTests
{
    public class AdvertisementTests : BaseTest
    {
        public AdvertisementTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Get_ads_returns_4()
        {
            DeleteData();
            AddData();
            var mockSender = SetupMock();
            AdvertisementController ctr = new AdvertisementController(UoW, mockSender.Object);
            OkObjectResult result = ctr.GetAdvertisementFromPharmacies() as OkObjectResult;
            var resultContent = result.Value as List<AdvertisementDto>;
            resultContent.Count.ShouldBe(4);
            DeleteData();
        }

        private Mock<IHttpRequestSender> SetupMock()
        {
            List<AdvertisementDto> dto1 = new List<AdvertisementDto>();
            dto1.Add(new AdvertisementDto
            {
                Description = "TestDesc1",
                MedicationName = "Aspirin1",
                Title = "TestTitle1"
            });
            dto1.Add(new AdvertisementDto
            {
                Description = "TestDesc2",
                MedicationName = "Aspirin2",
                Title = "TestTitle2"
            });
            List<AdvertisementDto> dto2 = new List<AdvertisementDto>();
            dto2.Add(new AdvertisementDto
            {
                Description = "TestDesc3",
                MedicationName = "Aspirin3",
                Title = "TestTitle3"
            });
            dto2.Add(new AdvertisementDto
            {
                Description = "TestDesc4",
                MedicationName = "Aspirin4",
                Title = "TestTitle4"
            });
            Mock<IHttpRequestSender> mockSender = new Mock<IHttpRequestSender>();
            RestResponse response1 = new RestResponse();
            response1.StatusCode = HttpStatusCode.OK;
            response1.Content = JsonConvert.SerializeObject(dto1);
            RestResponse response2 = new RestResponse();
            response2.StatusCode = HttpStatusCode.OK;
            response2.Content = JsonConvert.SerializeObject(dto2);
            RestResponse response3 = new RestResponse();
            response3.StatusCode = HttpStatusCode.OK;
            response3.Content = JsonConvert.SerializeObject(new List<AdvertisementDto>());
            var phar1 = UoW.GetRepository<IPharmacyReadRepository>().GetByName("AD_INT_TEST_PHARMACY1").FirstOrDefault();
            var phar2 = UoW.GetRepository<IPharmacyReadRepository>().GetByName("AD_INT_TEST_PHARMACY2").FirstOrDefault();
            List<string> urls = new List<string>();
            urls.Add(phar1.BaseUrl + "/api/Advertisement/GetAdvertisements?apiKey=" + phar1.ApiKey);
            urls.Add(phar2.BaseUrl + "/api/Advertisement/GetAdvertisements?apiKey=" + phar2.ApiKey);
            mockSender.Setup(m => m.Get(urls[0]))
                .Returns(response1);
            mockSender.Setup(m => m.Get(urls[1]))
                .Returns(response2);
            mockSender.Setup(m => m.Get(It.IsNotIn<string>(urls)))
                .Returns(response3);
            return mockSender;
        }

        private void AddData()
        {
            Country country = new Country
            {
                Name = "AD_INT_TEST_COUNTRY"
            };
            UoW.GetRepository<ICountryWriteRepository>().Add(country);
            City city = new City
            {
                Name = "AD_INT_TEST_CITY",
                CountryId = country.Id
            };
            UoW.GetRepository<ICityWriteRepository>().Add(city);
            Pharmacy pharmacy1 = new Pharmacy
            {
                Name = "AD_INT_TEST_PHARMACY1",
                ApiKey = new Guid(),
                BaseUrl = "AD_INT_TEST_URL1",
                CityId = city.Id
            };
            Pharmacy pharmacy2 = new Pharmacy
            {
                Name = "AD_INT_TEST_PHARMACY2",
                ApiKey = new Guid(),
                BaseUrl = "AD_INT_TEST_URL2",
                CityId = city.Id
            };
            var pharmacyRepo = UoW.GetRepository<IPharmacyWriteRepository>();
            pharmacyRepo.Add(pharmacy1);
            pharmacyRepo.Add(pharmacy2);
        }

        private void DeleteData()
        {
            var country = UoW.GetRepository<ICountryReadRepository>().GetByName("AD_INT_TEST_COUNTRY");
            if(country != null)UoW.GetRepository<ICountryWriteRepository>().Delete(country);
        }
    }
}
