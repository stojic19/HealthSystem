using Integration.Database;
using Integration.DTO;
using Integration.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Integration.Repositories.Base;
using Integration.Repositories;
using Microsoft.EntityFrameworkCore;
using IntegrationAPI.DTO;
using IntegrationAPI.Adapters;

namespace Integration.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PharmacyCommunicationController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public PharmacyCommunicationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult RegisterPharmacy(PharmacyDTO pharmacyDTO)
        {
            Pharmacy existingPharmacy = FindPharmacyByName(pharmacyDTO.Name);
            if (existingPharmacy != null)
            {
                return BadRequest("Pharmacy already exists!");
            }

            Pharmacy pharmacy = PharmacyAdapter.PharmacyDTOToPharmacy(pharmacyDTO);
            pharmacy.ApiKey = Guid.NewGuid();
            string hospitalUrl = "http://localhost:5000/";
            HospitalDTO dto = CreatePostData(pharmacyDTO, pharmacy, hospitalUrl);

            IRestResponse response = SendRegistrationPost(pharmacyDTO, dto);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return Ok();
            }

            SavePharmacy(pharmacy);
            return Ok("Pharmacy registered");
        }

        private static HospitalDTO CreatePostData(PharmacyDTO pharmacyDTO, Pharmacy pharmacy, string hospitalUrl)
        {
            Country country = new Country { Name = "Srbija" };
            City city = new City { Name = "Novi Sad", PostalCode = 21000 };
            HospitalDTO dto = new HospitalDTO { ApiKey = pharmacy.ApiKey, BaseUrl = hospitalUrl, Name = "Nasa bolnica", StreetName = "Vojvode Stepe", StreetNumber = "14", City = city };
            dto.City.Country = pharmacyDTO.City.Country;
            return dto;
        }

        private static IRestResponse SendRegistrationPost(PharmacyDTO pharmacyDTO, HospitalDTO dto)
        {
            RestClient client = new RestClient();
            string targetUrl = pharmacyDTO.BaseUrl + "/api/HospitalCommunication/AcceptHospitalRegistration";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(dto);
            return client.Post(request);
        }

        private void SavePharmacy(Pharmacy pharmacy)
        {
            Country country = FindCountryByName(pharmacy);
            City existingCity = FindCityByName(pharmacy);
            LinkEntities(pharmacy, country, existingCity);

            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyWriteRepository>();
            pharmacyRepo.Add(pharmacy);
        }

        private static void LinkEntities(Pharmacy pharmacy, Country country, City existingCity)
        {
            if (existingCity != null)
            {
                LinkCity(pharmacy, country, existingCity);
            }
            else if (country != null)
            {
                LinkCountry(pharmacy.City, country);
            }
        }

        private static void LinkCountry(City city, Country country)
        {
            city.Country = country;
            city.CountryId = country.Id;
        }

        private static void LinkCity(Pharmacy pharmacy, Country country, City existingCity)
        {
            LinkCountry(existingCity, country);
            pharmacy.CityId = existingCity.Id;
            pharmacy.City = existingCity;
        }

        private City FindCityByName(Pharmacy pharmacy)
        {
            var cityRepo = unitOfWork.GetRepository<ICityReadRepository>();
            DbSet<City> cities = cityRepo.GetAll();
            City existingCity = cities.FirstOrDefault(city => city.Name.Equals(pharmacy.City.Name));
            return existingCity;
        }

        private Country FindCountryByName(Pharmacy pharmacy)
        {
            var countryRepo = unitOfWork.GetRepository<ICountryReadRepository>();
            DbSet<Country> countries = countryRepo.GetAll();
            Country country = countries.FirstOrDefault(country => country.Name.Equals(pharmacy.City.Country.Name));
            return country;
        }

        [HttpGet]
        public IActionResult PingPharmacy(string pharmacyName)
        {
            Pharmacy existingPharmacy = FindPharmacyByName(pharmacyName);

            if (existingPharmacy == null) return BadRequest("Pharmacy with that name does not exist in database");
            IRestResponse response = SendPingToHospital(existingPharmacy);
            return Ok(response.Content);
        }

        private static IRestResponse SendPingToHospital(Pharmacy existingPharmacy)
        {
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(existingPharmacy.BaseUrl + "/api/hospitalCommunication/PingResponse/?apiKey=" + existingPharmacy.ApiKey);
            IRestResponse response = client.Get(request);
            return response;
        }

        private Pharmacy FindPharmacyByName(string pharmacyName)
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            DbSet<Pharmacy> existingPharmacies = pharmacyRepo.GetAll();
            Pharmacy existingPharmacy = existingPharmacies.FirstOrDefault(pharmacy => pharmacy.Name.Equals(pharmacyName));
            return existingPharmacy;
        }

        [HttpGet]
        public IActionResult PingResponse(string apiKey)
        {
            Pharmacy existingPharmacy = FindPharmacyByApiKey(apiKey);

            if (existingPharmacy == null)
            {
                return Ok("Pharmacy is not registered");
            }

            return Ok("Hospital responds to ping from " + existingPharmacy.Name);
        }

        [HttpPost]
        public IActionResult PostComplaint(CreateComplaintDTO createComplaintDTO)
        {
            Complaint complaint = CreateComplaint(createComplaintDTO);

            var complaintRepo = unitOfWork.GetRepository<IComplaintWriteRepository>();
            complaintRepo.Add(complaint);

            ComplaintDTO complaintDTO = new ComplaintDTO
            { ApiKey = complaint.Pharmacy.ApiKey.ToString(), CreatedDate = complaint.CreatedDate, Description = complaint.Description, Title = complaint.Title, ComplaintId = complaint.Id };
            
            IRestResponse response = SendComplaintToHospital(complaint, complaintDTO);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                complaintRepo.Delete(complaint);
                return BadRequest("Pharmacy failed to receive complaint! Try again");
            }

            return Ok("Complaint saved and sent to pharmacy!");
        }

        private static IRestResponse SendComplaintToHospital(Complaint complaint, ComplaintDTO complaintDTO)
        {
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(complaint.Pharmacy.BaseUrl + "/api/hospitalCommunication/PostComplaint");
            request.AddJsonBody(complaintDTO);
            IRestResponse response = client.Post(request);
            return response;
        }

        private Complaint CreateComplaint(CreateComplaintDTO createComplaintDTO)
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            Complaint complaint = new Complaint();
            complaint.Description = createComplaintDTO.Description;
            complaint.Title = createComplaintDTO.Title;
            complaint.Pharmacy = pharmacyRepo.GetById(createComplaintDTO.PharmacyId);
            complaint.CreatedDate = DateTime.Now;
            complaint.ManagerId = Program.ManagerId;
            return complaint;
        }

        [HttpPost]
        public IActionResult PostComplaintResponse(ComplaintResponseDTO complaintResponseDTO)
        {
            Pharmacy pharmacy = FindPharmacyByApiKey(complaintResponseDTO.ApiKey);
            if (pharmacy == null) return BadRequest("Pharmacy not registered");
            FindComplaint(complaintResponseDTO);
            SaveComplaintResponse(complaintResponseDTO);

            return Ok("Complaint response received!");
        }

        private Pharmacy FindPharmacyByApiKey(string apiKey)
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            DbSet<Pharmacy> existingPharmacies = pharmacyRepo.GetAll();
            Pharmacy pharmacy = existingPharmacies.FirstOrDefault(pharmacy => pharmacy.ApiKey.ToString().Equals(apiKey));
            return pharmacy;
        }

        private void FindComplaint(ComplaintResponseDTO complaintResponseDTO)
        {
            var complaintRepo = unitOfWork.GetRepository<IComplaintReadRepository>();
            Complaint complaint = complaintRepo.GetById(complaintResponseDTO.HospitalComplaintId);
        }

        private void SaveComplaintResponse(ComplaintResponseDTO complaintResponseDTO)
        {
            ComplaintResponse complaintResponse = new ComplaintResponse
            { CreatedDate = complaintResponseDTO.createdDate, Text = complaintResponseDTO.Text, ComplaintId = complaintResponseDTO.HospitalComplaintId };
            var responseRepo = unitOfWork.GetRepository<IComplaintResponseWriteRepository>();
            responseRepo.Add(complaintResponse);
        }
    }
}
