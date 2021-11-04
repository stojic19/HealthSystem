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

namespace Integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyCommunicationController : ControllerBase
    {
        private readonly HospitalDbContext dbContext;

        public PharmacyCommunicationController(HospitalDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet("RequestApiKey")]
        public IActionResult RequestApiKey(string url, string pharmacyName)
        {
            string targetUrl = url + "/api/hospitalCommunication/GetApiKey";
            string hospitalUrl = "http://" + HttpContext.Request.Headers["Host"];
            RestClient pharmacy = new RestClient();
            RestRequest request = new RestRequest(targetUrl + "/?hospitalName=" + Program.HospitalName + "&hospitalUrl=" + hospitalUrl);
            IRestResponse apiKey = pharmacy.Get(request);
            //HttpClient pharmacy = new HttpClient();
            /*Task<HttpResponseMessage> result = pharmacy.GetAsync(targetUrl + "/?hospitalName=" + Program.HospitalName + "&hospitalUrl=" + hospitalUrl);
            string apiKey = result.Result.Content.ReadAsStringAsync().Result;
            if (result.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest("This hospital is already registered in that pharmacy");
            }*/
            Pharmacy newPharmacy = new Pharmacy { ApiKey = Guid.Parse(apiKey.Content.Substring(1, apiKey.Content.Length - 2)), Name = pharmacyName, BaseUrl = url };
            dbContext.Pharmacies.Add(newPharmacy);
            dbContext.SaveChanges();
            return Ok(newPharmacy);
        }

        [HttpGet("PingPharmacy")]
        public IActionResult PingPharmacy(string pharmacyName)
        {
            Pharmacy pharmacy = dbContext.Pharmacies.FirstOrDefault(pharmacy => pharmacy.Name.Equals(pharmacyName));
            if (pharmacy == null) return BadRequest("Pharmacy with that name does not exist in database");
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(pharmacy.BaseUrl + "/api/hospitalCommunication/PingResponse/?apiKey=" + pharmacy.ApiKey);
            IRestResponse response = client.Get(request);
            return Ok(response.Content);
            /*Pharmacy pharmacy = dbContext.Pharmacies.FirstOrDefault(pharmacy => pharmacy.Name.Equals(pharmacyName));
            if (pharmacy == null) return BadRequest("Pharmacy with that name does not exist in database");
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> result = client.GetAsync(pharmacy.NetworkAdress + "/api/hospitalCommunication/PingResponse/?apiKey=" + pharmacy.ApiKey);
            string response = result.Result.Content.ReadAsStringAsync().Result;
            return Ok(response);*/
        }

        [HttpGet("PingResponse")]
        public IActionResult PingResponse(string apiKey)
        {
            Pharmacy pharmacy = dbContext.Pharmacies.FirstOrDefault(pharmacy => pharmacy.ApiKey.Equals(apiKey));
            if (pharmacy == null)
            {
                return Ok("Pharmacy is not registered");
            }
            return Ok("Hospital responds to ping");
        }

        //Ne radi
        [HttpPost("PostComplaint")]
        public IActionResult PostComplaint()
        {
            CreateComplaintDTO createComplaintdto = new CreateComplaintDTO { Title = "Zalba", Description = "Opis zalbe", PharmacyName = "pharmacy1" };
            Complaint complaint = new Complaint();
            //complaint.Pharmacy.Name = createComplaintdto.PharmacyName;
            complaint.Description = createComplaintdto.Description;
            complaint.Title = createComplaintdto.Title;
            complaint.Pharmacy = dbContext.Pharmacies.FirstOrDefault(pharmacy => pharmacy.Name.Equals(createComplaintdto.PharmacyName));
            complaint.CreatedDate = DateTime.Now;
            ComplaintDTO dto = new ComplaintDTO 
            { ApiKey = complaint.Pharmacy.ApiKey.ToString(), CreatedDate = complaint.CreatedDate, Description = complaint.Description, Title = complaint.Title };
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(complaint.Pharmacy.BaseUrl + "/api/hospitalCommunication/PostComplaint");
            request.AddJsonBody(dto);
            IRestResponse response = client.Post(request);
            /*var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = client.PostAsync(complaint.Pharmacy.NetworkAdress + "/hospitalCommunication/PostComplaint", content);
            string response = result.Result.Content.ReadAsStringAsync().Result;*/
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) return BadRequest(response);
            dbContext.Complaints.Add(complaint);
            dbContext.SaveChanges();
            return Ok(response);
        }


        //Test metode
        [HttpGet("GetAppUrl")]
        public IActionResult GetAppUrl()
        {
            return Ok(HttpContext.Request.Headers["Host"]);
        }

        /*[HttpGet("test")]
        public IActionResult test()
        {
            Hospital hospital = new Hospital();
            hospital.Name = "hospital";
            hospital.Id = 2;
            hospital.ApiKey = "...";
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest("http://localhost:32689/api/hospitalCommunication/postTest");
            request.AddJsonBody(hospital);
            IRestResponse response = client.Post(request);
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(hospital), Encoding.UTF8, "application/json");
            var result = client.PostAsync("http://localhost:32689/api/hospitalCommunication/postTest", content);
            string response = result.Result.Content.ReadAsStringAsync().Result;
            return Ok(response.Content);
        }*/
    }
}
