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

            Medication medicine = unitOfWork.GetRepository<IMedicationReadRepository>().GetMedicationByName(MedicineName);
            
            if(medicine == null)
            {
                var medicineWriteRepo = unitOfWork.GetRepository<IMedicationWriteRepository>();
                Medication newMedicine = new() {
                    Name = MedicineName, 
                    HowToUse = "", 
                    TimesPerDay = 0, 
                    MedicationIngredients = new List<MedicationIngredient>()
                };
                medicineWriteRepo.Add(newMedicine);

                newMedicine = unitOfWork.GetRepository<IMedicationReadRepository>().GetMedicationByName(MedicineName);
                MedicationInventory newMedicineInventory = new() 
                {
                    Medication = newMedicine,
                    MedicationId = newMedicine.Id, 
                    Quantity = Quantity
                };
                unitOfWork.GetRepository<IMedicationInventoryWriteRepository>().Add(newMedicineInventory);
            }
            else
            {
                MedicationInventory updateMedicineInventory = unitOfWork.GetRepository<IMedicationInventoryReadRepository>().GetMedicationByMedicationId(medicine.Id);
                updateMedicineInventory.Quantity += Quantity;
                unitOfWork.GetRepository<IMedicationInventoryWriteRepository>().Update(updateMedicineInventory);
            }
        }
    }
}
