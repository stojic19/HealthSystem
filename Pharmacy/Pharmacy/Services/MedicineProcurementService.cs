using Pharmacy.Exceptions;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class MedicineProcurementService
    {
        private readonly IUnitOfWork _uow;

        public MedicineProcurementService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public bool IsMedicineAvailable(string medicineName, string manufacturerName, int quantity)
        {
            Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetMedicineByNameAndManufacturerName(medicineName, manufacturerName);

            if (medicine == null)
            {
                throw new MedicineFromManufacturerNotFoundException();
            }

            return medicine.Quantity >= quantity;
        }

        public void ExecuteProcurement(string medicineName, string manufacturerName, int quantity)
        {
            if (!IsMedicineAvailable(medicineName, manufacturerName, quantity))
            {
                throw new MedicineUnavailableException();
            }

            Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetMedicineByNameAndManufacturerName(medicineName, manufacturerName);

            medicine.Quantity -= quantity;

            _uow.GetRepository<IMedicineWriteRepository>().Update(medicine);
        }
    }
}
