using System;
using System.Collections.Generic;
using System.IO;
using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using ceTe.DynamicPDF.PageElements;
using IntegrationAPI.Controllers.Base;
using IntegrationAPI.DTO.MedicineSpecification;
using IntegrationAPI.HttpRequestSenders;
using IntegrationApi.Messages;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using Path = System.IO.Path;

namespace IntegrationAPI.Controllers.Medicine
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineSpecificationController : BaseIntegrationController
    {
        public MedicineSpecificationController(IUnitOfWork unitOfWork, IHttpRequestSender sender) : base(unitOfWork, sender) { }

        [HttpGet]
        public IEnumerable<MedicineSpecificationFrontDTO> GetAllMedicineSpecificationFiles()
        {
            var allSpecFiles = 
                _unitOfWork.GetRepository<IMedicineSpecificationFileReadRepository>()
                .GetAll()
                .Include(x => x.Pharmacy);
            List<MedicineSpecificationFrontDTO> retVal = new List<MedicineSpecificationFrontDTO>();
            foreach (MedicineSpecificationFile medicineSpecificationFile in allSpecFiles)
            {
                retVal.Add(new MedicineSpecificationFrontDTO
                {
                    Id = medicineSpecificationFile.Id,
                    MedicineName = medicineSpecificationFile.MedicineName,
                    PharmacyName = medicineSpecificationFile.Pharmacy.Name,
                    FileName = medicineSpecificationFile.FileName,
                    ReceivedDate = medicineSpecificationFile.ReceivedDate
                });
            }

            return retVal;
        }

        [HttpPost, Produces("application/pdf")]
        public IActionResult GetSpecificationPdf([FromQuery(Name = "fileId")] int id)
        {
            var specificationFile = _unitOfWork.GetRepository<IMedicineSpecificationFileReadRepository>().GetById(id);
            if (specificationFile == null) return NotFound("Invalid id");
            try
            {
                var stream = new FileStream("MedicineSpecifications" + Path.DirectorySeparatorChar + specificationFile.FileName, FileMode.Open);
                return File(stream, "application/pdf", specificationFile.FileName);
            }
            catch
            {
                return NotFound(MedicineSpecificationMessages.FileNotFound);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult SendMedicineSpecificationRequest(MedicineSpecificationRequestDTO dto)
        {
            Pharmacy pharmacy = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetById(dto.PharmacyId);
            if (pharmacy == null) return BadRequest(PharmacyMessages.WrongId);
            var response = _httpRequestSender.Post(
                pharmacy.BaseUrl + "/api/MedicineSpecification/MedicineSpecificationRequest",
                new MedicineSpecificationToPharmacyDTO
                    {ApiKey = pharmacy.ApiKey, MedicineName = dto.MedicineName});
            if (response.StatusCode == HttpStatusCode.BadRequest) return NotFound(PharmacyMessages.MedicineNotFound);
            if (response.StatusCode == HttpStatusCode.InternalServerError) return NotFound(PharmacyMessages.FileNotSent);
            if (response.StatusCode != HttpStatusCode.OK) return NotFound(PharmacyMessages.CannotReach);
            MedicineSpecificationFileDTO medicineSpecificationFile =
                JsonConvert.DeserializeObject<MedicineSpecificationFileDTO>(response.Content);
            try
            {
                DownloadSpecificationFile(medicineSpecificationFile);
            }
            catch
            {
                return Problem(MedicineSpecificationMessages.CannotSaveFile);
            }
            SaveSpecificationFileInfo(medicineSpecificationFile, pharmacy);
            return Ok(MedicineSpecificationMessages.Success);
        }

        private void SaveSpecificationFileInfo(MedicineSpecificationFileDTO medicineSpecificationFile, Pharmacy pharmacy)
        {
            _unitOfWork.GetRepository<IMedicineSpecificationFileWriteRepository>().Add(new MedicineSpecificationFile
            {
                FileName = medicineSpecificationFile.FileName,
                Host = medicineSpecificationFile.Host,
                PharmacyId = pharmacy.Id,
                MedicineName = medicineSpecificationFile.MedicineName,
                ReceivedDate = medicineSpecificationFile.Date
            });
        }

        private void DownloadSpecificationFile(MedicineSpecificationFileDTO medicineSpecificationFile)
        {
            SftpClient sftpClient = new SftpClient(new PasswordConnectionInfo(_sftpCredentials.Host, _sftpCredentials.Username,
                _sftpCredentials.Password));
            sftpClient.Connect();
            Stream fileStream = System.IO.File.OpenWrite("MedicineSpecifications" + Path.DirectorySeparatorChar +
                                                         medicineSpecificationFile.FileName);
            sftpClient.DownloadFile(medicineSpecificationFile.FileName, fileStream);
            sftpClient.Disconnect();
            fileStream.Close();
        }
    }
}
