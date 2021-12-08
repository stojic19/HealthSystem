using Grpc.Core;
using PharmacyAPI.Protos;
using Pharmacy.Exceptions;
using Pharmacy.Repositories.Base;
using Pharmacy.Services;
using PharmacyApi.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.GrpcServices
{
    public class MedicineInventoryServiceImpl : MedicineInventoryService.MedicineInventoryServiceBase
    {
        private readonly HospitalAuthService _hospitalAuthService;
        private readonly MedicineProcurementService _medicineProcurementService;

        public MedicineInventoryServiceImpl()
        {

        }

        public MedicineInventoryServiceImpl(IUnitOfWork uow)
        {
            _hospitalAuthService = new HospitalAuthService(uow);
            _medicineProcurementService = new MedicineProcurementService(uow);
        }

        public override Task<CheckMedicineAvailabilityResponseModel> CheckMedicineAvailability(CheckMedicineAvailabilityRequestModel request, ServerCallContext context)
        {
            CheckMedicineAvailabilityResponseModel response = new CheckMedicineAvailabilityResponseModel();
            try
            {
                //_hospitalAuthService.ValidateApiKey(Guid.Parse(request.ApiKey));
                //bool answer = _medicineProcurementService.IsMedicineAvailable(request.MedicineName, request.ManufacturerName, request.Quantity);
                response.Answer = true; 
                System.Diagnostics.Debug.WriteLine(response);
                return Task.FromResult(response);
            }
            catch (UnauthorizedAccessException exception)
            {
                response.Answer = false;
                return Task.FromResult(response);
            }
            catch (MedicineFromManufacturerNotFoundException exception)
            {
                response.Answer = false;
                return Task.FromResult(response);
            }
        }

        public override Task<MedicineProcurementResponseModel> EmergencyMedicineProcurement(MedicineProcurementRequestModel request, ServerCallContext context)
        {
            try
            {
                _hospitalAuthService.ValidateApiKey(Guid.Parse(request.ApiKey));
                _medicineProcurementService.ExecuteProcurement(request.MedicineName, request.ManufacturerName, (int)request.Quantity);

                return Task.FromResult(new MedicineProcurementResponseModel { Answer = true });
            }
            catch (UnauthorizedAccessException exception)
            {
                return Task.FromResult(new MedicineProcurementResponseModel { Answer = false });
            }
            catch (MedicineFromManufacturerNotFoundException exception)
            {
                return Task.FromResult(new MedicineProcurementResponseModel { Answer = false });
            }
            catch (MedicineUnavailableException exception)
            {
                return Task.FromResult(new MedicineProcurementResponseModel { Answer = false });
            }
        }
    }
}
