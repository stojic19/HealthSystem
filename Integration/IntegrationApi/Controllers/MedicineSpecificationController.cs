using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using IntegrationAPI.DTO;
using Newtonsoft.Json;
using RestSharp;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineSpecificationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineSpecificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Produces("application/json")]
        public IActionResult SendMedicineSpecificationRequest(MedicineSpecificationRequestDTO dto)
        {
            Pharmacy pharmacy = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetById(dto.PharmacyId);
            if (pharmacy == null)
            {
                ModelState.AddModelError("Pharmacy Id", "Pharmacy with that id does not exist");
                return BadRequest(ModelState);
            }
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(pharmacy.BaseUrl + "/api/MedicineSpecification/MedicineSpecificationRequest");
            request.AddJsonBody(new MedicineSpecificationToPharmacyDTO
                {ApiKey = pharmacy.ApiKey, MedicineName = dto.MedicineName});
            var response = client.Post(request);
            if (response.StatusCode != HttpStatusCode.OK) return Ok("Failed to reach pharmacy or pharmacy does not have medicine with given name!");
            MedicineSpecificationFileDTO medicineSpecificationFile =
                JsonConvert.DeserializeObject<MedicineSpecificationFileDTO>(response.Content);
            _unitOfWork.GetRepository<IMedicineSpecificationFileWriteRepository>().Add(new MedicineSpecificationFile
            {
                FileName = medicineSpecificationFile.FileName,
                Host = medicineSpecificationFile.Host,
                PharmacyId = pharmacy.Id
            });
            return Ok("Pharmacy has sent the specification file to sftp server");
        }
    }
}
