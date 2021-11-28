using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpsPolicy;
using Pharmacy.Model;
using Pharmacy.Repositories;
using PharmacyApi.DTO;
using PharmacyIntegrationTests.Base;
using Shouldly;
using Xunit;

namespace PharmacyIntegrationTests
{
    public class ComplaintControllerTests : BaseTest
    {
        public ComplaintControllerTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Create_complaint_should_return_200()
        {
            var hospital = InsertHospital("Test hospital");

            ClearComplaintsWithTitle("testing");

            var newComplaint = new ComplaintDTO()
            {
                ApiKey = hospital.ApiKey,
                CreatedDateTime = DateTime.Now,
                Description = "testing the scenario",
                Title = "testing"
            };

            var content = GetContent(newComplaint);

            var response = await Client.PostAsync(BaseUrl + "api/Complaint/CreateComplaint", content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var foundComplaint = UoW.GetRepository<IComplaintReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Title == newComplaint.Title && x.Hospital.ApiKey == newComplaint.ApiKey);
            foundComplaint.ShouldNotBeNull();
        }

        [Fact]
        public async Task Create_complaint_should_return_400()
        {
            var newComplaint = new ComplaintDTO()
            {
                CreatedDateTime = DateTime.Now,
                Title = "testing",
            };

            var content = GetContent(newComplaint);

            var response = await Client.PostAsync(BaseUrl + "api/Complaint/CreateComplaint", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_complaint_response_should_return_200()
        {
            var hospital = InsertHospital("Test hospital");

            var complaint = InsertComplaint("test complaint", hospital.Id);

            var newComplaintResponse = new CreateComplaintResponseDTO()
            {
                Description = "test complaint response",
                ComplaintId = complaint.Id,
            };

            var content = GetContent(newComplaintResponse);

            var response = await Client.PostAsync(BaseUrl + "api/Complaint/CreateComplaintResponse", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var foundComplaintResponse = UoW.GetRepository<IComplaintResponseReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.ComplaintId == complaint.Id);
            foundComplaintResponse.ShouldNotBeNull();
        }

        private Complaint InsertComplaint(string complaintTitle, int hospitalId)
        {
            var complaint = UoW.GetRepository<IComplaintReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.HospitalId == hospitalId && x.Title == complaintTitle);

            if (complaint == null)
            {
                complaint = new Complaint()
                {
                    CreatedDate = DateTime.Now,
                    Description = "Test description",
                    HospitalId = hospitalId,
                    Title = complaintTitle
                };
                UoW.GetRepository<IComplaintWriteRepository>().Add(complaint);
            }

            var complaintResponse = UoW.GetRepository<IComplaintResponseReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.ComplaintId == complaint.Id);

            if (complaintResponse != null)
            {
                UoW.GetRepository<IComplaintResponseWriteRepository>().Delete(complaintResponse);
            }

            return complaint;
        }

        private void ClearComplaintsWithTitle(string title)
        {
            var complaints = UoW.GetRepository<IComplaintReadRepository>()
                .GetAll()
                .Where(x => x.Title.ToLower() == title.ToLower());

            var complaintsResponses = UoW.GetRepository<IComplaintResponseReadRepository>()
                .GetAll()
                .Where(x => x.Complaint.Title.ToLower() == title.ToLower());

            UoW.GetRepository<IComplaintResponseWriteRepository>().DeleteRange(complaintsResponses);
            UoW.GetRepository<IComplaintWriteRepository>().DeleteRange(complaints);
        }

        private Hospital InsertHospital(string hospitalName)
        {
            var hospital = UoW.GetRepository<IHospitalReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == hospitalName);

            if (hospital == null)
            {
                var city = UoW.GetRepository<ICityReadRepository>()
                    .GetAll()
                    .FirstOrDefault();

                if (city == null)
                {
                    var country = UoW.GetRepository<ICountryReadRepository>()
                        .GetAll()
                        .FirstOrDefault();

                    if (country == null)
                    {
                        country = new Country()
                        {
                            Name = "Test country"
                        };
                        UoW.GetRepository<ICountryWriteRepository>().Add(country);
                    }

                    city = new City()
                    {
                        CountryId = country.Id,
                        Name = "Test city",
                        PostalCode = 10
                    };
                    UoW.GetRepository<ICityWriteRepository>().Add(city);
                }

                hospital = new Hospital()
                {
                    ApiKey = Guid.NewGuid(),
                    CityId = city.Id,
                    Name = hospitalName,
                    StreetName = "str",
                    StreetNumber = "12b"
                };
                UoW.GetRepository<IHospitalWriteRepository>().Add(hospital);
            }

            return hospital;
        }
    }
}
