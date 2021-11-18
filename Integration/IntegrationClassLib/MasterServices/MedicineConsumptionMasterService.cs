using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Integration.MicroServices;
using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;

namespace Integration.MasterServices
{
    public class MedicineConsumptionMasterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineConsumptionMasterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MedicineConsumptionReport CreateConsumptionReportInTimeRange(TimeRange timeRange)
        {
            MedicineConsumptionCalculationMicroService medicineConsumptionCalculationMicroService = new MedicineConsumptionCalculationMicroService();
            var repo = _unitOfWork.GetRepository<IReceiptReadRepository>();
            IEnumerable<Receipt> receiptsInTimeRange = repo.GetReceiptLogsInTimeRange(timeRange);
            IEnumerable<MedicineConsumption> medicineConsumptions =
                medicineConsumptionCalculationMicroService.CalculateMedicineConsumptions(receiptsInTimeRange);
            MedicineConsumptionReport retVal = new MedicineConsumptionReport
            {
                startDate = timeRange.startDate,
                endDate = timeRange.endDate,
                createdDate = DateTime.Now,
                MedicineConsumptions = medicineConsumptions
            };
            return retVal;
        }
    }
}
