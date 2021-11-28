using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;
using System;
using System.Collections.Generic;

namespace Integration.Partnership.Service
{
    public class MedicineReportingMasterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ReceiptMicroService _receiptMicroService;

        public MedicineReportingMasterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MedicineConsumptionReport CreateMedicineConsumptionReport(TimeRange timeRange)
        {
            var receiptRepo = _unitOfWork.GetRepository<IReceiptReadRepository>();
            IEnumerable<Receipt> allReceiptLogs = receiptRepo.GetAll();
            IEnumerable<Receipt> receiptsInTimeRange =
                _receiptMicroService.GetReceiptLogsInTimeRange(timeRange, allReceiptLogs);
            IEnumerable<MedicineConsumption> medicineConsumptions =
                _receiptMicroService.CalculateMedicineConsumptions(receiptsInTimeRange);
            MedicineConsumptionReport medicineConsumptionReport = new MedicineConsumptionReport
            {
                startDate = timeRange.startDate,
                endDate = timeRange.endDate,
                createdDate = DateTime.Now,
                MedicineConsumptions = medicineConsumptions
            };
            return medicineConsumptionReport;
        }
    }
}
