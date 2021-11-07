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
        public string RegisterPharmacy(PharmacyDTO pharmacyDTO)
        {
            var repo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            DbSet<Pharmacy> existingPharmacies = repo.GetAll();
            Pharmacy existingPharmacy = existingPharmacies.FirstOrDefault(pharmacy => pharmacy.Name.Equals(pharmacyDTO.Name));
            if(existingPharmacy != null)
            {
                return "Pharmacy already exists!";
            }
            Pharmacy pharmacy = PharmacyAdapter.PharmacyDTOToPharmacy(pharmacyDTO);
            pharmacy.ApiKey = Guid.NewGuid();
            string hospitalUrl = "http://localhost:5000/";
            Country country = new Country { Name = "Srbija" };
            City city = new City { Name = "Novi Sad", PostalCode = 21000 };
            HospitalDTO dto = new HospitalDTO { ApiKey = pharmacy.ApiKey, BaseUrl = hospitalUrl, Name = "Nasa bolnica", StreetName = "Vojvode Stepe", StreetNumber = "14", City = city };
            dto.City.Country = pharmacyDTO.City.Country;
            IRestResponse response = SendRegistrationPost(pharmacyDTO, dto);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "Hospital failed to register at pharmacy, possible reason: hospital is already registered";
            }
            SavePharmacy(pharmacy);
            return "Pharmacy registered";
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
            var countryRepo = unitOfWork.GetRepository<ICountryReadRepository>();
            DbSet<Country> countries = countryRepo.GetAll();
            Country country = countries.FirstOrDefault(country => country.Name.Equals(pharmacy.City.Country.Name));
            var cityRepo = unitOfWork.GetRepository<ICityReadRepository>();
            DbSet<City> cities = cityRepo.GetAll();
            City existingCity = cities.FirstOrDefault(city => city.Name.Equals(pharmacy.City.Name));
            if(existingCity != null)
            {
                existingCity.CountryId = country.Id;
                existingCity.Country = country;
                pharmacy.CityId = existingCity.Id;
                pharmacy.City = existingCity;
            }
            else if(country != null)
            {
                pharmacy.City.Country = country;
                pharmacy.City.CountryId = country.Id;
            }
            var repo = unitOfWork.GetRepository<IPharmacyWriteRepository>();
            repo.Add(pharmacy);
        }

        [HttpGet]
        public IActionResult PingPharmacy(string pharmacyName)
        {
            var repo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            DbSet<Pharmacy> existingPharmacies = repo.GetAll();
            Pharmacy existingPharmacy = existingPharmacies.FirstOrDefault(pharmacy => pharmacy.Name.Equals(pharmacyName));
            if (existingPharmacy == null) return BadRequest("Pharmacy with that name does not exist in database");
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(existingPharmacy.BaseUrl + "/api/hospitalCommunication/PingResponse/?apiKey=" + existingPharmacy.ApiKey);
            IRestResponse response = client.Get(request);
            return Ok(response.Content);
        }

        [HttpGet]
        public IActionResult PingResponse(string apiKey)
        {
            var repo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            DbSet<Pharmacy> existingPharmacies = repo.GetAll();
            Pharmacy existingPharmacy = existingPharmacies.FirstOrDefault(pharmacy => pharmacy.ApiKey.ToString().Equals(apiKey));
            if (existingPharmacy == null)
            {
                return Ok("Pharmacy is not registered");
            }
            return Ok("Hospital responds to ping from " + existingPharmacy.Name);
        }

        [HttpPost]
        public IActionResult PostComplaint(CreateComplaintDTO createComplaintDTO)
        {
            var repo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            Complaint complaint = new Complaint();
            complaint.Description = createComplaintDTO.Description;
            complaint.Title = createComplaintDTO.Title;
            complaint.Pharmacy = repo.GetById(createComplaintDTO.PharmacyId);
            complaint.CreatedDate = DateTime.Now;
            complaint.ManagerId = Program.ManagerId;
            var writeRepo = unitOfWork.GetRepository<IComplaintWriteRepository>();
            writeRepo.Add(complaint);
            ComplaintDTO dto = new ComplaintDTO
            { ApiKey = complaint.Pharmacy.ApiKey.ToString(), CreatedDate = complaint.CreatedDate, Description = complaint.Description, Title = complaint.Title, ComplaintId = complaint.Id };
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(complaint.Pharmacy.BaseUrl + "/api/hospitalCommunication/PostComplaint");
            request.AddJsonBody(dto);
            IRestResponse response = client.Post(request);
            if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                writeRepo.Delete(complaint);
                return BadRequest("Pharmacy failed to receive complaint! Try again");
            }
            return Ok("Complaint saved and sent to pharmacy!");
        }

        [HttpPost]
        public IActionResult PostComplaintResponse(ComplaintResponseDTO dto)
        {           
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            DbSet<Pharmacy> existingPharmacies = pharmacyRepo.GetAll();
            Pharmacy pharmacy = existingPharmacies.FirstOrDefault(pharmacy => pharmacy.ApiKey.ToString().Equals(dto.ApiKey));
            if (pharmacy == null) return BadRequest("Pharmacy not registered");

            var complaintRepo = unitOfWork.GetRepository<IComplaintReadRepository>();
            Complaint complaint = complaintRepo.GetById(dto.HospitalComplaintId);

            ComplaintResponse complaintResponse = new ComplaintResponse { CreatedDate = dto.createdDate, Text = dto.Text, ComplaintId = dto.HospitalComplaintId };
            var responseRepo = unitOfWork.GetRepository<IComplaintResponseWriteRepository>();
            responseRepo.Add(complaintResponse);

            return Ok("Complaint response received!");
        }
    }
}
