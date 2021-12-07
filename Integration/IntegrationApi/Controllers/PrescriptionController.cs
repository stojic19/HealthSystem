using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IntegrationAPI.Adapters.PDF;
using IntegrationAPI.Adapters.PDF.Implementation;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private IUnitOfWork _uow;

        public PrescriptionController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public IActionResult PostPrescription(PrescriptionDTO dto)
        {
            var pharmacies = _uow.GetRepository<IPharmacyReadRepository>().GetAll();
            Pharmacy foundPharmacy = null;
            foreach(var pharmacy in pharmacies)
            {
                CreateMedicineRequestForPharmacyDTO medicineDto = new CreateMedicineRequestForPharmacyDTO()
                {
                    PharmacyId = pharmacy.Id,
                    MedicineName = dto.MedicineName,
                    Quantity = 1,
                    ManufacturerName = "Manufacturer 1"
                };
                RestClient restClient = new RestClient();
                RestRequest request = new RestRequest($"{Request.Scheme}://{Request.Host}" + "/api/Medicine/RequestMedicineInformation");
                request.AddJsonBody(medicineDto);
                var response = restClient.Post(request);
                var content = response.Content;
                Console.WriteLine(content);
                if((HttpStatusCode)response.GetType().GetProperty("StatusCode").GetValue(response, null) != HttpStatusCode.OK)
                {
                    continue;
                }
                else
                {

                    CheckMedicineAvailabilityResponseDTO responseDTO = 
                        JsonConvert.DeserializeObject<CheckMedicineAvailabilityResponseDTO>(content);
                    if (responseDTO.answer)
                    {
                        foundPharmacy = pharmacy;
                        break;
                    }
                }
            }

            if (foundPharmacy == null)
            {
                return BadRequest("No pharmacy has the needed medicine");
            }

            var sftpResponse = SendPrescriptionWithSftp(foundPharmacy, dto);
            var httpResponse = SendPrescriptionWithHttp(foundPharmacy, dto);

            return Ok(foundPharmacy.Name + " " + sftpResponse + " " + httpResponse);
        }

        private string SendPrescriptionWithSftp(Pharmacy pharmacy, PrescriptionDTO dto)
        {
            IPDFAdapter adapter = new DynamicPDFAdapter();
            string fileName = adapter.MakePrescriptionPdf(dto, "sftp");
            return fileName;
        }

        private string SendPrescriptionWithHttp(Pharmacy pharmacy, PrescriptionDTO dto)
        {
            IPDFAdapter adapter = new DynamicPDFAdapter();
            string fileName = adapter.MakePrescriptionPdf(dto, "http");
            return fileName;
        }
    }
}
