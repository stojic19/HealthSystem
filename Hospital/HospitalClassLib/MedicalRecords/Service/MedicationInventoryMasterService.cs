using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;

namespace Hospital.MedicalRecords.Service
{
    public class MedicationInventoryMasterService
    {
        private readonly IUnitOfWork unitOfWork;
        public MedicationInventoryMasterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void AddMedicineToInventory(String MedicineName, int Quantity)
        {
            var medicineReadRepo = unitOfWork.GetRepository<IMedicationReadRepository>();
            Medication medicine = medicineReadRepo.GetMedicationByName(MedicineName);
            if(medicine == null)
            {
                var medicineWriteRepo = unitOfWork.GetRepository<IMedicationWriteRepository>();
                Medication newMedicine = new Medication { Name = MedicineName, HowToUse = "", TimesPerDay = 0, MedicationIngredients = new List<MedicationIngredient>() };
                medicineWriteRepo.Add(newMedicine);
                newMedicine = medicineReadRepo.GetMedicationByName(MedicineName);
                MedicationInventory newMedicineInventory = new MedicationInventory { Medication = newMedicine, MedicationId = newMedicine.Id, Quantity = Quantity };
                var medicineInventoryWriteRepo = unitOfWork.GetRepository<IMedicationInventoryWriteRepository>();
                medicineInventoryWriteRepo.Add(newMedicineInventory);
            }
            else
            {
                var medicineInventoryReadRepo = unitOfWork.GetRepository<IMedicationInventoryReadRepository>();
                MedicationInventory updateMedicineInventory = medicineInventoryReadRepo.GetMedicationByMedicationId(medicine.Id);
                updateMedicineInventory.Quantity += Quantity;
                var medicineInventoryWriteRepo = unitOfWork.GetRepository<IMedicationInventoryWriteRepository>();
                medicineInventoryWriteRepo.Update(updateMedicineInventory);
            }
        }
    }
}
