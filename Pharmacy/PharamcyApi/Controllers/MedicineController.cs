using System;
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
        public Medicine Add(Medicine medicine)
        {
            return _uow.GetRepository<IMedicineWriteRepository>().Add(medicine);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Medicine> medicines = _uow.GetRepository<IMedicineReadRepository>().GetAll().Include(medicine => medicine.Manufacturer);

            if (medicines.ToList().Count == 0)
                return NotFound();

            return Ok(medicines);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .FirstOrDefault(medicine => medicine.Id == id);

            if (medicine == null)
                return NotFound();

            return Ok(medicine);
        }

        [HttpGet]
        public IActionResult GetByName(string name)
        {
            Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .FirstOrDefault(medicine => medicine.Name.Equals(name));
            
            if (medicine == null)
                return NotFound();
            
            return Ok(medicine);
        }

        [HttpGet]
        public IActionResult GetFilteredMedicine(string medicineName, string substanceName, string medicineType, string manufacturerName)
        {
            IEnumerable<Medicine> medicines = _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .Where(medicine => String.IsNullOrEmpty(medicineName) || medicine.Name.Equals(medicineName))
                .Where(medicine => String.IsNullOrEmpty(substanceName) || medicine.Substances.Contains(substanceName))
                .Where(medicine => String.IsNullOrEmpty(medicineType) || medicine.Type.Equals(medicineType))
                .Where(medicine => String.IsNullOrEmpty(manufacturerName) || medicine.Manufacturer.Name.Equals(manufacturerName));

            if (medicines.ToList().Count == 0)
                return NotFound();

            return Ok(medicines);
        }

        [HttpGet]
        public IActionResult GetFilteredMedicineWithPaging(string medicineName, string substanceName, string medicineType, string manufacturerName, int pageNumber, int pageSize)
        {
            IEnumerable<Medicine> medicines = _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .Where(medicine => String.IsNullOrEmpty(medicineName) || medicine.Name.Equals(medicineName))
                .Where(medicine => String.IsNullOrEmpty(substanceName) || medicine.Substances.Contains(substanceName))
                .Where(medicine => String.IsNullOrEmpty(medicineType) || medicine.Type.Equals(medicineType))
                .Where(medicine => String.IsNullOrEmpty(manufacturerName) || medicine.Manufacturer.Name.Equals(manufacturerName))
                .Skip((pageNumber - 1) * pageSize);

            if (medicines.ToList().Count == 0)
                return NotFound();

            return Ok(medicines);
        }
    }
}
