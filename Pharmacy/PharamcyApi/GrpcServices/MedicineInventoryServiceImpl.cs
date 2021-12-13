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
                _hospitalAuthService.ValidateApiKey(Guid.Parse(request.ApiKey));
                bool answer = _medicineProcurementService.IsMedicineAvailable(request.MedicineName, request.ManufacturerName, (int)request.Quantity);
                return Task.FromResult(new CheckMedicineAvailabilityResponseModel() { Answer = answer, ExceptionMessage = ""});
            }
            catch (UnauthorizedAccessException exception)
            {
                return Task.FromResult(new CheckMedicineAvailabilityResponseModel() { Answer = false, ExceptionMessage = exception.Message });
            }
            catch (MedicineFromManufacturerNotFoundException exception)
            {
                return Task.FromResult(new CheckMedicineAvailabilityResponseModel() { Answer = false, ExceptionMessage = exception.Message });
            }
        }

        public override Task<MedicineProcurementResponseModel> EmergencyMedicineProcurement(MedicineProcurementRequestModel request, ServerCallContext context)
        {
            try
            {
                _hospitalAuthService.ValidateApiKey(Guid.Parse(request.ApiKey));
                _medicineProcurementService.ExecuteProcurement(request.MedicineName, request.ManufacturerName, (int)request.Quantity);

                return Task.FromResult(new MedicineProcurementResponseModel { Answer = true , ExceptionMessage = "" });
            }
            catch (UnauthorizedAccessException exception)
            {
                return Task.FromResult(new MedicineProcurementResponseModel { Answer = false, ExceptionMessage = exception.Message });
            }
            catch (MedicineFromManufacturerNotFoundException exception)
            {
                return Task.FromResult(new MedicineProcurementResponseModel { Answer = false, ExceptionMessage = exception.Message });
            }
            catch (MedicineUnavailableException exception)
            {
                return Task.FromResult(new MedicineProcurementResponseModel { Answer = false, ExceptionMessage = exception.Message });
            }
        }
    }
}
