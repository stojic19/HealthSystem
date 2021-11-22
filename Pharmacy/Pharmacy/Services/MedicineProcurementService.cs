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
            Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetAll()
                .FirstOrDefault(medicine => medicine.Name.Equals(medicineName) && medicine.Manufacturer.Name.Equals(manufacturerName));

            if (medicine == null)
            {
                throw new KeyNotFoundException("Medicine with given name from given manufacturer not found.");
            }

            return medicine.Quantity >= quantity;
        }
    }
}
