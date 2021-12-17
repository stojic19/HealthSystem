using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IntegrationAPI.Adapters.PDF;
using IntegrationAPI.Adapters.PDF.Implementation;
using IntegrationAPI.Controllers.Base;
using IntegrationAPI.DTO.Prescription;
using Renci.SshNet;

namespace IntegrationAPI.Controllers.Medicine
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrescriptionController : BaseIntegrationController
    {

        public PrescriptionController(IUnitOfWork uow) : base(uow){}

        [HttpPost]
        public IActionResult PostPrescription(PrescriptionDTO dto)
        {
            var pharmacies = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll();
            Pharmacy foundPharmacy = null;
            foreach(var pharmacy in pharmacies)
            {
                if (CheckMedicineInPharmacy(dto, pharmacy))
                {
                    foundPharmacy = pharmacy;
                    break;
                }
            }

            if (foundPharmacy == null)
            {
                return BadRequest("No pharmacy has the needed medicine");
            }

            var sftpResponse = SendPrescriptionWithSftp(foundPharmacy, dto);
            
            var httpResponse = SendPrescriptionWithHttp(foundPharmacy, dto);

            string fullResponse = "Pharmacy: " + foundPharmacy.Name + "\n" +
                                  "SFTP: " + sftpResponse + "\n" +
                                  "HTTP: " + httpResponse;
            if (sftpResponse.Equals("Done") || httpResponse.Equals("Done"))
            {
                return Ok(fullResponse);
            }
            else
            {
                return BadRequest(fullResponse);
            }
            
        }

        private bool CheckMedicineInPharmacy(PrescriptionDTO dto, Pharmacy pharmacy)
        {
            CreateMedicineRequestForPharmacyDto medicineDto = new CreateMedicineRequestForPharmacyDto()
            {
                PharmacyId = pharmacy.Id,
                MedicineName = dto.MedicineName,
                Quantity = 1,
                ManufacturerName = "Manufacturer 1"
            };
            try
            {
                RestClient restClient = new RestClient();
                RestRequest request =
                    new RestRequest($"{Request.Scheme}://{Request.Host}" + "/api/Medicine/RequestMedicineInformation");
                request.AddJsonBody(medicineDto);
                var response = restClient.Post(request);
                var content = response.Content;
                Console.WriteLine(content);
                if ((HttpStatusCode) response.GetType().GetProperty("StatusCode").GetValue(response, null) !=
                    HttpStatusCode.OK)
                {
                    return false;
                }

                CheckMedicineAvailabilityResponseDto responseDTO =
                    JsonConvert.DeserializeObject<CheckMedicineAvailabilityResponseDto>(content);
                if (responseDTO.Answer)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        private string SendPrescriptionWithSftp(Pharmacy pharmacy, PrescriptionDTO dto)
        {
            try
            {
                IPDFAdapter adapter = new DynamicPDFAdapter();
                string fileName = adapter.MakePrescriptionPdf(dto, "sftp");
                string path = "Prescriptions" + Path.DirectorySeparatorChar + "Sftp" + Path.DirectorySeparatorChar +
                              fileName;
                SftpClient sftpClient = new SftpClient(new PasswordConnectionInfo(_sftpCredentials.Host,
                    _sftpCredentials.Username, _sftpCredentials.Password));
                sftpClient.Connect();
                Stream fileStream = System.IO.File.OpenRead(path);
                string filePath = Path.GetFileName(path);
                sftpClient.UploadFile(fileStream, filePath);
                sftpClient.Disconnect();
                fileStream.Close();

                PrescriptionSendSftpDto prescDto = new PrescriptionSendSftpDto()
                {
                    ApiKey = pharmacy.ApiKey,
                    FileName = fileName
                };
                RestClient client = new RestClient();
                string targetUrl = pharmacy.BaseUrl + "/api/Prescription/ReceivePrescriptionSftp";
                RestRequest request = new RestRequest(targetUrl);
                request.AddJsonBody(prescDto);
                var response = client.Post(request).Content;

                return response;
            }
            catch
            {
                return "Cannot send prescription";
            }
            
        }

        private string SendPrescriptionWithHttp(Pharmacy pharmacy, PrescriptionDTO dto)
        {
            try
            {
                IPDFAdapter adapter = new DynamicPDFAdapter();
                string fileName = adapter.MakePrescriptionPdf(dto, "http");

                byte[] file = System.IO.File.ReadAllBytes("Prescriptions" + Path.DirectorySeparatorChar + "Http" +
                                                          Path.DirectorySeparatorChar + fileName);
                PrescriptionSendHttpDto prescDto = new PrescriptionSendHttpDto()
                {
                    ApiKey = pharmacy.ApiKey,
                    FileContent = file,
                    FileName = fileName
                };
                RestClient client = new RestClient();
                string targetUrl = pharmacy.BaseUrl + "/api/Prescription/ReceivePrescriptionHttp";
                RestRequest request = new RestRequest(targetUrl);
                request.AddJsonBody(prescDto);
                var response = client.Post(request).Content;

                return response;
            }
            catch
            {
                return "Cannot send prescription";
            }
        }
    }
}
