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
using IntegrationAPI.HttpRequestSenders;
using IntegrationApi.Messages;
using Renci.SshNet;

namespace IntegrationAPI.Controllers.Medicine
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrescriptionController : BaseIntegrationController
    {

        public PrescriptionController(IUnitOfWork uow, IHttpRequestSender sender) : base(uow, sender){}

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

            if (foundPharmacy == null) return BadRequest(PrescriptionMessages.NoMedicineInAnyPharmacy);

            var sftpResponse = SendPrescriptionWithSftp(foundPharmacy, dto);
            var httpResponse = SendPrescriptionWithHttp(foundPharmacy, dto);

            var fullResponse = CreateResponse(foundPharmacy, sftpResponse, httpResponse);
            if (sftpResponse.Contains("Done") || httpResponse.Contains("Done")) return Ok(fullResponse);
            return BadRequest(fullResponse);
        }

        private static string CreateResponse(Pharmacy foundPharmacy, string sftpResponse, string httpResponse)
        {
            string fullResponse = "Pharmacy: " + foundPharmacy.Name + "\n" +
                                  "SFTP: " + sftpResponse + "\n" +
                                  "HTTP: " + httpResponse;
            return fullResponse;
        }

        private bool CheckMedicineInPharmacy(PrescriptionDTO dto, Pharmacy pharmacy)
        {
            var medicineDto = CreateMedicineRequestForPharmacyDto(dto, pharmacy);
            try
            {
                var response = _httpRequestSender.Post($"{Request.Scheme}://{Request.Host}" + "/api/Medicine/RequestMedicineInformation", medicineDto);
                var content = response.Content;
                if (response.StatusCode != HttpStatusCode.OK) return false;

                CheckMedicineAvailabilityResponseDto responseDTO =
                    JsonConvert.DeserializeObject<CheckMedicineAvailabilityResponseDto>(content);
                return responseDTO.Answer;
            }
            catch
            {
                return false;
            }
        }

        private static CreateMedicineRequestForPharmacyDto CreateMedicineRequestForPharmacyDto(PrescriptionDTO dto, Pharmacy pharmacy)
        {
            CreateMedicineRequestForPharmacyDto medicineDto = new CreateMedicineRequestForPharmacyDto()
            {
                PharmacyId = pharmacy.Id,
                MedicineName = dto.MedicineName,
                Quantity = 1,
                ManufacturerName = "Manufacturer 1"
            };
            return medicineDto;
        }

        private string SendPrescriptionWithSftp(Pharmacy pharmacy, PrescriptionDTO dto)
        {
            try
            {
                IPDFAdapter adapter = new DynamicPDFAdapter();
                string fileName = adapter.MakePrescriptionPdf(dto, "sftp");
                string path = "Prescriptions" + Path.DirectorySeparatorChar + "Sftp" + Path.DirectorySeparatorChar +
                              fileName;
                SendPrescriptionToSftpServer(path);

                PrescriptionSendSftpDto prescDto = new PrescriptionSendSftpDto()
                {
                    ApiKey = pharmacy.ApiKey,
                    FileName = fileName
                };
                var response = _httpRequestSender.Post(pharmacy.BaseUrl + "/api/Prescription/ReceivePrescriptionSftp", prescDto);
                return response.Content;
            }
            catch
            {
                return PrescriptionMessages.CannotSend;
            }
            
        }

        private void SendPrescriptionToSftpServer(string path)
        {
            SftpClient sftpClient = new SftpClient(new PasswordConnectionInfo(_sftpCredentials.Host,
                _sftpCredentials.Username, _sftpCredentials.Password));
            sftpClient.Connect();
            Stream fileStream = System.IO.File.OpenRead(path);
            string filePath = Path.GetFileName(path);
            sftpClient.UploadFile(fileStream, filePath);
            sftpClient.Disconnect();
            fileStream.Close();
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
                var response = _httpRequestSender.Post(pharmacy.BaseUrl + "/api/Prescription/ReceivePrescriptionHttp", prescDto);
                return response.Content;
            }
            catch
            {
                return PrescriptionMessages.CannotSend;
            }
        }
    }
}
