using Integration.Pharmacies.Model;
using Integration.Pharmacies.Service;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System;

namespace IntegrationAPI.Controllers.Pharmacies
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private PharmacyMasterService _pharmacyMasterService;

        public PharmacyController(IUnitOfWork unitOfWork)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
        }

        [HttpGet]
        public IEnumerable<Pharmacy> GetPharmacies()
        {
            return _pharmacyMasterService.GetPharmacies();
        }

        [HttpGet("{id:int}")]
        public Pharmacy GetPharmacyById(int id)
        {
            return _pharmacyMasterService.GetPharmacyById(id);
        }

        [HttpPost]
        public IActionResult UpdatePharmacy(UpdatePharmacyDTO updatePharmacyDTO)
        {
            if (updatePharmacyDTO.PharmacyName != "")
            {
                Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(updatePharmacyDTO.Id);
                pharmacy.Name = updatePharmacyDTO.PharmacyName;
                pharmacy.StreetName = updatePharmacyDTO.StreetName;
                pharmacy.StreetNumber = updatePharmacyDTO.StreetNumber;
                pharmacy.City = new City { PostalCode = updatePharmacyDTO.PostalCode, Name = updatePharmacyDTO.CityName, Country = new Country { Name = updatePharmacyDTO.CountryName } };
                pharmacy.Description = updatePharmacyDTO.Description;
                pharmacy.ImageName = updatePharmacyDTO.ImageName;
                _pharmacyMasterService.UpdatePharmacy(pharmacy);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult UploadImage()
        {
            var file = Request.Form.Files[0];
            string pathToFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            string fullPath = Path.Combine(pathToFolder, file.FileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok();
        }

        [HttpPost]
        public string GetImage(PharmacyIdDTO pharmacyIdDTO)
        {
            try {
                Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(pharmacyIdDTO.PharmacyId);
                string pathToFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                string fullPath = Path.Combine(pathToFolder, pharmacy.ImageName);
                var bytes = System.IO.File.ReadAllBytes(fullPath);
                string file = Convert.ToBase64String(bytes);

                return JsonSerializer.Serialize(file);
            }
            catch
            {
                return JsonSerializer.Serialize("err");
            }
        }
    }
}
