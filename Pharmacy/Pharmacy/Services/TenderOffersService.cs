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
    public class TenderOffersService
    {
        private readonly IUnitOfWork _uow;

        public TenderOffersService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void CreateTenderOffer(Guid hospitalApiKey,string medicineName,int quantity,DateTime creationTime)
        {
            Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetMedicineByName(medicineName);
            if (medicine == null) throw new MedicineFromManufacturerNotFoundException();
            if (medicine.Quantity < quantity) throw new MedicineUnavailableException();

            Hospital hospital = _uow.GetRepository<IHospitalReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.ApiKey == hospitalApiKey);

            TenderOffer tenderOffer = new TenderOffer()
            {
                MedicineId = medicine.Id,
                Medicine = medicine,
                HospitalId = hospital.Id,
                Hospital = hospital,
                Quantity = quantity,
                CreationTime = creationTime,
                IsConfirmed = false
            };

            _uow.GetRepository<ITenderOfferWriteRepository>().Add(tenderOffer);

        }

    }
}
