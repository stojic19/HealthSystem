using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Pharmacy.Exceptions;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Services
{
    public class AdvertisementService
    {
        private readonly IUnitOfWork _uow;

        public AdvertisementService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void CreateAdvertisement(string title, string description, int medicineId)
        {
            if (_uow.GetRepository<IMedicineReadRepository>().GetById(medicineId) == null) throw new MedicineNotFoundException();
            var advertisement = new Advertisement()
            {
                Title = title,
                Description = description,
                MedicineId = medicineId
            };
            _uow.GetRepository<IAdvertisementWriteRepository>().Add(advertisement);
        }

        public void DeleteAdvertisement(int id)
        {
            var advertisement = _uow.GetRepository<IAdvertisementReadRepository>().GetById(id);
            if(advertisement == null) throw new AdvertisementNotFoundException();

            _uow.GetRepository<IAdvertisementWriteRepository>().Delete(advertisement);
        }

        public void UpdateAdvertisement(int id,string title,string description)
        {
            var advertisement = _uow.GetRepository<IAdvertisementReadRepository>().GetById(id);
            if (advertisement == null) throw new AdvertisementNotFoundException();

            advertisement.Description = description;
            advertisement.Title = title;
            _uow.GetRepository<IAdvertisementWriteRepository>().Update(advertisement);
        }
    }
}
