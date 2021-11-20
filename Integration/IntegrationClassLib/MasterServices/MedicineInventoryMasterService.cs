using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.MasterServices
{
    public class MedicineInventoryMasterService
    {
        private readonly IUnitOfWork unitOfWork;
        public MedicineInventoryMasterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void AddMedicineToInventory(String MedicineName, int Quantity)
        {
            var medicineReadRepo = unitOfWork.GetRepository<IMedicineReadRepository>();
            Medicine medicine = medicineReadRepo.GetMedicineByName(MedicineName);
            if(medicine == null)
            {
                var medicineWriteRepo = unitOfWork.GetRepository<IMedicineWriteRepository>();
                Medicine newMedicine = new Medicine { Name = MedicineName};
                medicineWriteRepo.Add(newMedicine);
                newMedicine = medicineReadRepo.GetMedicineByName(MedicineName);
                MedicineInventory newMedicineInventory = new MedicineInventory { Medicine = newMedicine, MedicineId = newMedicine.Id, Quantity = Quantity };
                var medicineInventoryWriteRepo = unitOfWork.GetRepository<IMedicineInventoryWriteRepository>();
                medicineInventoryWriteRepo.Add(newMedicineInventory);
            }
            else
            {
                var medicineInventoryReadRepo = unitOfWork.GetRepository<IMedicineInventoryReadRepository>();
                MedicineInventory updateMedicineInventory = medicineInventoryReadRepo.GetMedicineByMedicineId(medicine.Id);
                updateMedicineInventory.Quantity += Quantity;
                var medicineInventoryWriteRepo = unitOfWork.GetRepository<IMedicineInventoryWriteRepository>();
                medicineInventoryWriteRepo.Update(updateMedicineInventory);
            }
        }
    }
}
