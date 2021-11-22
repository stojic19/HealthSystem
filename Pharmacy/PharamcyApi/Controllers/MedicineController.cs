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
                return ValidationProblem("Manufacturer doesn't exist!");
            }

            if (!IsMedicineUnique(medicineDTO.Name)) return ValidationProblem("Medicine already exists!");

            //create
            Medicine medicineCreated = CreateNewMedicine(medicineDTO, manufacturerId);
            _uow.GetRepository<IMedicineWriteRepository>().Add(medicineCreated);


            return Ok("Medicine succesfully created!");
        }

       

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Medicine> medicines = _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .Include(medicine => medicine.SideEffects)
                .Include(medicine => medicine.Reactions)
                .Include(medicine => medicine.Substances)
                .Include(medicine => medicine.Precautions)
                .Include(medicine => medicine.MedicinePotentialDangers);

            if (medicines == null || medicines.Count() == 0)
                return NotFound();

            return Ok(medicines);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .Include(medicine => medicine.SideEffects)
                .Include(medicine => medicine.Reactions)
                .Include(medicine => medicine.Substances)
                .Include(medicine => medicine.Precautions)
                .Include(medicine => medicine.MedicinePotentialDangers)
                .FirstOrDefault(medicine => medicine.Id == id);

            if (medicine == null)
                return NotFound();

            return Ok(medicine);
        }

        [HttpGet]
        public IActionResult GetFilteredMedicine(string medicineName, string substanceName, string medicineType, string manufacturerName)
        {
            IEnumerable<Medicine> medicines = _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .Include(medicine => medicine.SideEffects)
                .Include(medicine => medicine.Reactions)
                .Include(medicine => medicine.Substances)
                .Include(medicine => medicine.Precautions)
                .Include(medicine => medicine.MedicinePotentialDangers)
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
            IEnumerable<Medicine> medicines = _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .Include(medicine => medicine.Manufacturer)
                .Include(medicine => medicine.SideEffects)
                .Include(medicine => medicine.Reactions)
                .Include(medicine => medicine.Substances)
                .Include(medicine => medicine.Precautions)
                .Include(medicine => medicine.MedicinePotentialDangers)
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
            IEnumerable<MedicineCombination> medicineCombinations = _uow.GetRepository<IMedicineCombinationReadRepository>().GetAll()
                .Include(medicine => medicine.FirstMedicine)
                .Include(medicine => medicine.SecondMedicine)
                .Where(medicine => medicine.FirstMedicine.Name.Equals(firstMedicine) || medicine.SecondMedicine.Name.Equals(firstMedicine));

            List<Medicine> medicines = new List<Medicine>();

            foreach(var combination in medicineCombinations)
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


        private Medicine CreateNewMedicine(CreateMedicineDTO medicineDTO, int manufacturerId)
        {
            return new Medicine()
            {
                Name = medicineDTO.Name,
                ManufacturerId = manufacturerId,
                SideEffects = GenerateObjects(medicineDTO.SideEffects,_uow.GetRepository<ISideEffectReadRepository>().GetSideEffectByName),
                Reactions = GenerateObjects(medicineDTO.Reactions, _uow.GetRepository<IReactionReadRepository>().GetReactionByName),
                Usage = medicineDTO.Usage,
                WeightInMilligrams = medicineDTO.WeightInMilligrams,
                Precautions = GenerateObjects(medicineDTO.Precautions, _uow.GetRepository<IPrecautionReadRepository>().GetPrecautionByName),
                MedicinePotentialDangers = GenerateObjects(medicineDTO.MedicinePotentialDangers, _uow.GetRepository<IMedicinePotentialDangerReadRepository>().GetMedicinePotentialDangerByName),
                Substances = GenerateObjects(medicineDTO.Substances, _uow.GetRepository<ISubstanceReadRepository>().GetSubstanceByName),
                Type = medicineDTO.Type,
                Quantity = medicineDTO.Quantity
            };
        }

        


   

        private List<T> GenerateObjects<T>(List<string> strings, Func<string, T> action) where T: class,new()
        {
            var sideEffects = new List<T>();
            foreach (string s in strings)
            {
                var sideEffect = action.Invoke(s);
                if (sideEffect != null)
                    sideEffects.Add(sideEffect);
                else
                    sideEffects.Add((T)Activator.CreateInstance(typeof(T),new object[] { s }));
            }

            return sideEffects;
        }



    }
}
