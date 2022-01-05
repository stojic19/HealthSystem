using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineController : BasePharmacyController
    {

        public MedicineController(IUnitOfWork uow, PharmacyDetails details) : base(uow, details)
        {
        }

        [HttpPost]
        public IActionResult Create(CreateMedicineDTO medicineDTO)
        {

            if (!IsApiKeyValid(medicineDTO.ApiKey))
                return BadRequest(ModelState);
            

            int manufacturerId;
            try
            {
                manufacturerId = FindManufacturer(medicineDTO.ManufacturerName);
            }
            catch
            {
                return ValidationProblem("Manufacturer doesn't exist!");
            }

            if (!IsMedicineUnique(medicineDTO.Name)) return ValidationProblem("Medicine already exists!");

            
            Medicine medicineCreated = CreateNewMedicine(medicineDTO, manufacturerId);
            UoW.GetRepository<IMedicineWriteRepository>().Add(medicineCreated);


            return Ok("Medicine succesfully created!");
        }

        [HttpDelete]
        public IActionResult RemoveByName(Guid APIKey,string medicineName)
        {
            if (!IsApiKeyValid(APIKey))
                return BadRequest(ModelState);

            if (IsMedicineUnique(medicineName)) return ValidationProblem("Medicine with given name doesn't exist!");

            var removedMedicine = UoW.GetRepository<IMedicineReadRepository>().GetMedicineByName(medicineName);
            UoW.GetRepository<IMedicineWriteRepository>().Delete(removedMedicine);

            return Ok("Medicine removed succesfully!");
        }

        [HttpPut]
        public IActionResult Update(UpdateMedicineDTO updateMedicineDTO)
        {
            if (!IsApiKeyValid(updateMedicineDTO.ApiKey))
                return BadRequest(ModelState);

            if (IsMedicineUnique(updateMedicineDTO.Name)) return ValidationProblem("Medicine with given name doesn't exist!");

            var updatedMedicine = CreateUpdatedMedicine(updateMedicineDTO);
            UoW.GetRepository<IMedicineWriteRepository>().Update(updatedMedicine);

            return Ok("Medicine updated succesfully!");
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Medicine> medicines = UoW.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .Include(medicine => medicine.Substances);

            if (medicines == null || medicines.Count() == 0)
                return NotFound();

            return Ok(medicines);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            Medicine medicine = UoW.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .Include(medicine => medicine.Substances)
                .FirstOrDefault(medicine => medicine.Id == id);

            if (medicine == null)
                return NotFound();

            return Ok(medicine);
        }

        [HttpGet]
        public IActionResult GetFilteredMedicine(string medicineName, string substanceName, string medicineType, string manufacturerName)
        {
            IEnumerable<Medicine> medicines = UoW.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .Include(medicine => medicine.Substances)
                .Where(medicine => String.IsNullOrEmpty(medicineName) || medicine.Name.Equals(medicineName))
                .Where(medicine => String.IsNullOrEmpty(substanceName) || (medicine.Substances.Where(substance => substance.Name == substanceName).Any()))
                .Where(medicine => String.IsNullOrEmpty(medicineType) || medicine.Type.Equals(medicineType))
                .Where(medicine => String.IsNullOrEmpty(manufacturerName) || medicine.Manufacturer.Name.Equals(manufacturerName));

            if (medicines == null || medicines.Count() == 0)
                return NotFound();

            return Ok(medicines);
        }

        [HttpGet]
        public IActionResult GetFilteredMedicineWithPaging(string medicineName, string substanceName, string medicineType, string manufacturerName, int pageNumber, int pageSize)
        {
            IEnumerable<Medicine> medicines = UoW.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .Include(medicine => medicine.Substances)
                .Where(medicine => String.IsNullOrEmpty(medicineName) || medicine.Name.Equals(medicineName))
                .Where(medicine => String.IsNullOrEmpty(substanceName) || (medicine.Substances.Where(substance => substance.Name == substanceName).Any()))
                .Where(medicine => String.IsNullOrEmpty(medicineType) || medicine.Type.Equals(medicineType))
                .Where(medicine => String.IsNullOrEmpty(manufacturerName) || medicine.Manufacturer.Name.Equals(manufacturerName))
                .Skip((pageNumber - 1) * pageSize);

            if (medicines == null || medicines.Count() == 0)
                return NotFound();

            return Ok(medicines);
        }

        [HttpGet]
        public IActionResult GetMedicinesThatCanBeCombined(string firstMedicine)
        {
            IEnumerable<MedicineCombination> medicineCombinations = UoW.GetRepository<IMedicineCombinationReadRepository>().GetAll()
                .Include(medicine => medicine.FirstMedicine)
                .Include(medicine => medicine.SecondMedicine)
                .Where(medicine => medicine.FirstMedicine.Name.Equals(firstMedicine) || medicine.SecondMedicine.Name.Equals(firstMedicine));

            List<Medicine> medicines = new List<Medicine>();

            foreach (var combination in medicineCombinations)
            {
                if (combination.FirstMedicine.Name.Equals(firstMedicine))
                    medicines.Add(combination.SecondMedicine);
                else if (combination.SecondMedicine.Name.Equals(firstMedicine))
                    medicines.Add(combination.FirstMedicine);
            }

            if (medicines.Count() == 0)
                return NotFound();

            return Ok(medicines);
        }


        private int FindManufacturer(string ManufacturerName)
        {
            var manufacturer = UoW.GetRepository<IManufacturerReadRepository>().GetManufacturerByName(ManufacturerName);
            if (manufacturer != null) return manufacturer.Id;

            throw new System.Exception();
        }

        private bool IsMedicineUnique(string MedicineName)
        {
            var medicine = UoW.GetRepository<IMedicineReadRepository>().GetMedicineByName(MedicineName);
            if (medicine != null) return false;
            return true;
        }


        private Medicine CreateNewMedicine(CreateMedicineDTO medicineDTO, int manufacturerId)
        {
            return new Medicine()
            {
                Name = medicineDTO.Name,
                ManufacturerId = manufacturerId,
                SideEffects = medicineDTO.SideEffects,
                Reactions = medicineDTO.Reactions,
                Usage = medicineDTO.Usage,
                WeightInMilligrams = medicineDTO.WeightInMilligrams,
                Precautions = medicineDTO.Precautions,
                MedicinePotentialDangers = medicineDTO.MedicinePotentialDangers,
                Substances = GenerateObjects(medicineDTO.Substances, UoW.GetRepository<ISubstanceReadRepository>().GetSubstanceByName),
                Type = medicineDTO.Type,
                Quantity = medicineDTO.Quantity,
                Price = medicineDTO.Price
            };
        }



        private Medicine CreateUpdatedMedicine(UpdateMedicineDTO updateMedicineDTO)
        {
            var updatedMedicine = UoW.GetRepository<IMedicineReadRepository>().GetMedicineByName(updateMedicineDTO.Name);
            updatedMedicine.SideEffects = updateMedicineDTO.SideEffects;
            updatedMedicine.Reactions = updateMedicineDTO.Reactions;
            updatedMedicine.Usage = updateMedicineDTO.Usage;
            updatedMedicine.WeightInMilligrams = updateMedicineDTO.WeightInMilligrams;
            updatedMedicine.Precautions = updateMedicineDTO.Precautions;
            updatedMedicine.MedicinePotentialDangers = updateMedicineDTO.MedicinePotentialDangers;
            updatedMedicine.Quantity = updateMedicineDTO.Quantity;
            updatedMedicine.Price = updateMedicineDTO.Price;

            return updatedMedicine;
        }

        //currently used only for generating Substances objects
        private static List<T> GenerateObjects<T>(List<string> strings, Func<string, T> action) where T: class,new()
        {
            var retList = new List<T>();
            foreach (string s in strings)
            {
                var obj = action.Invoke(s);
                if (obj != null)
                    retList.Add(obj);
                else
                    retList.Add((T)Activator.CreateInstance(typeof(T),new object[] { s }));
            }

            return retList;
        }



    }
}
