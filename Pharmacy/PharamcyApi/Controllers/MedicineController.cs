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
            _uow.GetRepository<IMedicineWriteRepository>().Add(medicineCreated);


            return Ok("Medicine succesfully created!");
        }

        [HttpDelete]
        public IActionResult RemoveByName(string medicineName)
        {
            if (IsMedicineUnique(medicineName)) return ValidationProblem("Medicine with given name doesn't exist!");

            var removedMedicine = _uow.GetRepository<IMedicineReadRepository>().GetMedicineByName(medicineName);
            _uow.GetRepository<IMedicineWriteRepository>().Delete(removedMedicine);

            return Ok("Medicine removed succesfully!");
        }

        [HttpPut]
        public IActionResult Update(UpdateMedicineDTO updateMedicineDTO)
        {
            if (IsMedicineUnique(updateMedicineDTO.Name)) return ValidationProblem("Medicine with given name doesn't exist!");

            var updatedMedicine = CreateUpdatedMedicine(updateMedicineDTO);
            _uow.GetRepository<IMedicineWriteRepository>().Update(updatedMedicine);

            return Ok("Medicine updated succesfully!");
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

            if (medicines.Count() == 0)
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



        private Medicine CreateUpdatedMedicine(UpdateMedicineDTO updateMedicineDTO)
        {
            var updatedMedicine = _uow.GetRepository<IMedicineReadRepository>().GetMedicineByName(updateMedicineDTO.Name);
            updatedMedicine.SideEffects = GenerateObjects(updateMedicineDTO.SideEffects, _uow.GetRepository<ISideEffectReadRepository>().GetSideEffectByName);
            updatedMedicine.Reactions = GenerateObjects(updateMedicineDTO.Reactions, _uow.GetRepository<IReactionReadRepository>().GetReactionByName);
            updatedMedicine.Usage = updateMedicineDTO.Usage;
            updatedMedicine.WeightInMilligrams = updateMedicineDTO.WeightInMilligrams;
            updatedMedicine.Precautions = GenerateObjects(updateMedicineDTO.Precautions, _uow.GetRepository<IPrecautionReadRepository>().GetPrecautionByName);
            updatedMedicine.MedicinePotentialDangers = GenerateObjects(updateMedicineDTO.MedicinePotentialDangers, _uow.GetRepository<IMedicinePotentialDangerReadRepository>().GetMedicinePotentialDangerByName);
            updatedMedicine.Quantity = updateMedicineDTO.Quantity;

            return updatedMedicine;
        }


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
