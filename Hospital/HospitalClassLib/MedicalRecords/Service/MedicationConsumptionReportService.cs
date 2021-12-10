using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Service
{
    public class MedicationConsumptionReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicationConsumptionReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MedicationConsumptionReport CreateMedicationExpenditureReportInTimePeriod(TimePeriod timePeriod)
        {
            var logs = _unitOfWork
                .GetRepository<IMedicationExpenditureLogReadRepository>()
                .GetMedicationExpenditureLogsInTimePeriod(timePeriod);
            IEnumerable<MedicationConsumption> medicineConsumptions = new MedicationConsumptionService()
                .CalculateMedicationConsumptions(logs);
            return new MedicationConsumptionReport
            {
                MedicationConsumptions = medicineConsumptions,
                StartDate = timePeriod.StartTime,
                EndDate = timePeriod.EndTime,
                CreatedDate = DateTime.Now
            };
        }
    }
}
