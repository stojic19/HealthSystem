using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using PharmacyApi.DTO;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineController : Controller
    {
        private readonly IUnitOfWork _uow;

        public MedicineController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public IActionResult Create(CreateMedicineDTO medicineDTO)
        {
            //validate
            int manufacturerId;
            try
            {
                manufacturerId = FindManufacturer(medicineDTO.ManufacturerName);
            }
            catch
            {
                return BadRequest("Manufacturer doesn't exist!");
            }

            if (!IsMedicineUnique(medicineDTO.Name)) return BadRequest("Medicine already exists!");
            
            //create
            Medicine medicineCreated = CreateMedicine(medicineDTO, manufacturerId);
            _uow.GetRepository<IMedicineWriteRepository>().Add(medicineCreated);


            return Ok("Medicine succesfully created!");
        }

   

        [HttpGet]
        public IEnumerable<Medicine> GetAll()
        {
            return _uow.GetRepository<IMedicineReadRepository>().GetAll().Include(medicine => medicine.Manufacturer);
        }

        [HttpGet]
        public Medicine GetById(int id)
        {
            return _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .FirstOrDefault(medicine => medicine.Id == id);
        }

        [HttpGet]
        public Medicine GetByName(string name)
        {
            return _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .FirstOrDefault(medicine => medicine.Name.Equals(name));
        }

 
        private int FindManufacturer(string ManufacturerName)
        {
            var manufacturer = _uow.GetRepository<IManufacturerReadRepository>().GetManufacturerByName(ManufacturerName);
            if (manufacturer != null) return manufacturer.Id;

            throw new System.Exception();
        }

        private bool IsMedicineUnique(string MedicineName)
        {
            var medicine = _uow.GetRepository<IMedicineReadRepository>().GetMedicineByName(MedicineName);
            if (medicine != null) return false;
            return true;
        }


        private static Medicine CreateMedicine(CreateMedicineDTO medicineDTO, int manufacturerId)
        {
            return new Medicine()
            {
                Name = medicineDTO.Name,
                ManufacturerId = manufacturerId,
                SideEffects = medicineDTO.SideEffects,
                Reactions = medicineDTO.Reactions,
                Usage = medicineDTO.Usage,
                MedicinesThatCanBeCombined = medicineDTO.MedicinesThatCanBeCombined,
                WeightInMilligrams = medicineDTO.WeightInMilligrams,
                MainPrecautions = medicineDTO.MainPrecautions,
                PotentialDangers = medicineDTO.PotentialDangers,
                Substances = medicineDTO.Substances,
                Type = medicineDTO.Type,
                Quantity = medicineDTO.Quantity
            };
        }



    }
}
