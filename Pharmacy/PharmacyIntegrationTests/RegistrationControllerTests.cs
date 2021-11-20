using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pharmacy.Model;
using Pharmacy.Repositories;
using PharmacyApi.DTO;
using PharmacyIntegrationTests.Base;
using Shouldly;
using Xunit;

namespace PharmacyIntegrationTests
{
    public class RegistrationControllerTests : BaseTest
    {
        public RegistrationControllerTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task New_hospital_should_be_registered()
        {
            InsertCity("Novi Sad");

            CheckAndDeleteHospitals("Test hospital");

            var newHospitalDTO = new RegisterHospitalDTO()
            {
                Name = "Test hospital",
                CityName = "Novi Sad",
                StreetName = "test street",
                StreetNumber = "12",
                BaseUrl = "some url"
            };

            var content = GetContent(newHospitalDTO);

            var response = await Client.PostAsync(BaseUrl + "api/Registration/RegisterHospital",content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responsePharmacyDTO = JsonConvert.DeserializeObject<HospitalRegisteredDTO>(responseContent);

            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responsePharmacyDTO.ShouldNotBeNull();
            responsePharmacyDTO.CityName.ShouldNotBeNullOrEmpty();
            responsePharmacyDTO.StreetName.ShouldNotBeNullOrEmpty();
            responsePharmacyDTO.StreetNumber.ShouldNotBeNullOrEmpty();
            responsePharmacyDTO.CountryName.ShouldBe("Serbia");

            var hospitalInTheDb = UoW.GetRepository<IHospitalReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.ApiKey == responsePharmacyDTO.ApiKey);
            hospitalInTheDb.ShouldNotBeNull();
        }

        private void InsertCity(string name)
        {
            var city = UoW.GetRepository<ICityReadRepository>()
                .GetCityByName(name);

            if (city == null)
            {
                var country = UoW.GetRepository<ICountryReadRepository>()
                    .GetAll()
                    .FirstOrDefault(x => x.Name == "Serbia");
                if (country == null)
                {
                    country = UoW.GetRepository<ICountryWriteRepository>().Add(country, false);
                }

                city = new City()
                {
                    CountryId = country.Id,
                    Name = name,
                    PostalCode = 21000
                };
            }
        }

        private void CheckAndDeleteHospitals(string name)
        {
            var hospitals = UoW.GetRepository<IHospitalReadRepository>()
                .GetAll()
                .Where(x => x.Name == name);

            if (hospitals.Any())
            {
                UoW.GetRepository<IHospitalWriteRepository>()
                    .DeleteRange(hospitals);
            }
        }
    }
}
