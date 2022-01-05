using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IntegrationAPI.Adapters.PDF;
using IntegrationAPI.Adapters.PDF.Implementation;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using Pharmacy.Repositories.DbImplementation;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;
using Renci.SshNet;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineSpecificationController : BasePharmacyController
    {
        public MedicineSpecificationController(IUnitOfWork uow, PharmacyDetails details) : base(uow, details)
        {

        }

        [HttpPost]
        public IActionResult MedicineSpecificationRequest(SpecificationDTO dto)
        {
            if (!IsApiKeyValid(dto.ApiKey))
            {
                return BadRequest(ModelState);
            }

            Medicine medicine = UoW.GetRepository<IMedicineReadRepository>()
                .GetAll()
                .Include( x => x.Manufacturer)
                .Include(x=>x.Substances)
                .Where(x => x.Name.Equals(dto.MedicineName)).First();
            if (medicine == null)
            {
                ModelState.AddModelError("Medicine", "Medicine with given name does not exist");
                return BadRequest(ModelState);
            }
            SftpCredentialsDTO credentials = getSftpCredentials();
            MedicineDTO medicineDto = makeMedicineDTO(medicine);
            string fileName;
            try
            {
                fileName = saveMedicineSpecificationsToSftpServer(medicineDto, credentials);
            }
            catch
            {
                return BadRequest("Error while saving to sftp server");
            }
            MedicineSpecificationFileDTO medicineSpecificationFileDto = new MedicineSpecificationFileDTO
            {
                Host = credentials.Host,
                FileName = fileName,
                Date = DateTime.Now,
                MedicineName = medicine.Name
            };
            return Ok(medicineSpecificationFileDto);
        }

        private MedicineDTO makeMedicineDTO(Medicine medicine)
        {
            MedicineDTO dto = new MedicineDTO
            {
                Name = medicine.Name,
                Manufacturer = medicine.Manufacturer.Name,
                Type = medicine.Type,
                Usage = medicine.Usage,
                WeightInMilligrams = medicine.WeightInMilligrams,
                SideEffects = medicine.SideEffects,
                Reactions = medicine.Reactions,
                Precautions = medicine.Precautions,
                MedicinePotentialDangers = medicine.MedicinePotentialDangers,
                Substances = new List<string>(),
            };
            foreach (Substance substance in medicine.Substances)
            {
                dto.Substances.Add(substance.Name);
            }
            return dto;
        }

        private string saveMedicineSpecificationsToSftpServer(MedicineDTO medicine, SftpCredentialsDTO credentials)
        {
            //string fileName = medicine.Name + "Specification" + DateTime.Now.Ticks + ".txt";
            //string path = "MedicineSpecifications" + Path.DirectorySeparatorChar + fileName;
            //saveFile(medicine, path);
            IPDFAdapter adapter = new DynamicPDFAdapter();
            string fileName = adapter.MakeMedicineSpecificationPdf(medicine);
            string path = "MedicineSpecifications" + Path.DirectorySeparatorChar + fileName;
            saveToSftp(path, credentials);
            return fileName;
        }
        private void saveFile(MedicineDTO medicine, string path)
        {
            StreamWriter fileSaveStream = new StreamWriter(path);
            string jsonString = JsonConvert.SerializeObject(medicine);
            fileSaveStream.Write(jsonString);
            fileSaveStream.Close();
        }
        private void saveToSftp(string path, SftpCredentialsDTO credentials)
        {
            SftpClient sftpClient = new SftpClient(new PasswordConnectionInfo(credentials.Host, credentials.Username, credentials.Password));
            sftpClient.Connect();
            Stream fileStream = System.IO.File.OpenRead(path);
            string filePath = Path.GetFileName(path);
            sftpClient.UploadFile(fileStream, filePath);
            sftpClient.Disconnect();
            fileStream.Close();
        }
        private SftpCredentialsDTO getSftpCredentials()
        {
            return new SftpCredentialsDTO
            {
                Host = "192.168.0.13",
                Password = "password",
                Username = "tester"
            };
        }
    }
}
