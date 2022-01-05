using Newtonsoft.Json;
using Pharmacy.Exceptions;
using Pharmacy.Model;
using Pharmacy.Model.RabbitMQMessages;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Services
{
    public class TenderOffersService
    {
        private readonly IUnitOfWork _uow;

        public TenderOffersService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void CreateTenderOffer(Guid hospitalApiKey,List<MedicationRequest> medicationRequests,Money money, int tenderId)
        {
            foreach (MedicationRequest iterationRequest in medicationRequests)
            {
                Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetMedicineByName(iterationRequest.MedicineName);
                if (medicine == null) throw new MedicineFromManufacturerNotFoundException();
                if (medicine.Quantity < iterationRequest.Quantity) throw new MedicineUnavailableException();
            }
            Hospital hospital = _uow.GetRepository<IHospitalReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.ApiKey == hospitalApiKey);

            TenderOffer tenderOffer = new TenderOffer()
            {
                MedicationRequests = medicationRequests,
                TenderId = tenderId,
                CreationTime = DateTime.Now,
                Cost = money,
                IsWinning = false,
                IsConfirmed = false
            };

            _uow.GetRepository<ITenderOfferWriteRepository>().Add(tenderOffer);
            try
            {
                var factory = new ConnectionFactory() { HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("new tender offer", ExchangeType.Direct);
                    var tender = _uow.GetRepository<ITenderReadRepository>().GetById(tenderId);
                    NewTenderOfferMessage msg = new NewTenderOfferMessage
                    {
                        Apikey = hospital.ApiKey,
                        Cost = tenderOffer.Cost.Amount,
                        Currency = tenderOffer.Cost.Currency,
                        MedicationRequests = new List<MedicationRequestMessage>(),
                        TenderCreatedDate = tender.CreatedDate,
                        CreatedDate = tenderOffer.CreationTime
                    };
                    foreach(MedicationRequest medReq in tenderOffer.MedicationRequests)
                    {
                        msg.MedicationRequests.Add(new MedicationRequestMessage
                        {
                            MedicineName = medReq.MedicineName,
                            Quantity = medReq.Quantity
                        });
                    }
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));
                    channel.BasicPublish("new tender offer", hospital.ApiKey.ToString(), null, body);
                }
            }
            catch
            {
                throw new RabbitMQNewOfferException();
            }

        }

        public void ConfirmTenderOffer(Guid hospitalApiKey, int tenderOfferId)
        {
            TenderOffer tenderOffer = _uow.GetRepository<ITenderOfferReadRepository>()
                .GetAll()
                .Include(p => p.Tender)
                .FirstOrDefault(p => p.Id == tenderOfferId);
            CheckTender(tenderOffer);


            Hospital hospital = _uow.GetRepository<IHospitalReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.ApiKey == hospitalApiKey);
            if (hospital.Id != tenderOffer.Tender.HospitalId) throw new UnauthorizedAccessException();

            foreach (MedicationRequest iterationRequest in tenderOffer.MedicationRequests)
            {
                Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetMedicineByName(iterationRequest.MedicineName);
                if (iterationRequest.Quantity > medicine.Quantity) throw new MedicineUnavailableException();
            }

            var medRepo = _uow.GetRepository<IMedicineWriteRepository>();
            foreach (MedicationRequest iterationRequest in tenderOffer.MedicationRequests)
            {
                Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetMedicineByName(iterationRequest.MedicineName);
                medicine.Quantity -= iterationRequest.Quantity;
                medRepo.Update(medicine);
            }
            tenderOffer.IsConfirmed = true;
            _uow.GetRepository<ITenderOfferWriteRepository>().Update(tenderOffer);
        }

        public double GetTenderPrice(int tenderOfferId)
        {
            TenderOffer tenderOffer = _uow.GetRepository<ITenderOfferReadRepository>().GetById(tenderOfferId);
            CheckTender(tenderOffer);
            return tenderOffer.Cost.Amount;
            /*if(tenderOffer.Medicine.Quantity < tenderOffer.Quantity) throw new MedicineUnavailableException();

            return tenderOffer.Medicine.Price * tenderOffer.Quantity;*/
        }

        private static void CheckTender(TenderOffer tenderOffer)
        {
            if (tenderOffer == null) throw new TenderNotFoundException();
            if (tenderOffer.IsConfirmed) throw new TenderAlreadyEnabledException();
        }

    }
}
