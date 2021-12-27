using System.Collections.Generic;
using System.Linq;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service.Interfaces;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Service
{
    public class BlockingService : IBlockingService
    {
        private readonly IUnitOfWork _uow;
        public BlockingService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void BlockPatient(string userName)
        {
            var patient = _uow.GetRepository<IPatientReadRepository>().GetAll().FirstOrDefault(x => x.NameEquals(userName));
            if (patient == null) return;
            patient.Block();
            _uow.GetRepository<IPatientWriteRepository>().Update(patient);
        }

        public List<Patient> GetMaliciousPatients()
        {
            var patients = _uow.GetRepository<IPatientReadRepository>().GetAll().Where(x => x.IsBlocked == false).ToList();
            return (from patient in patients
                let numOfCanceledEventsInLastMonth = CalculateInLastMonthForPatient(patient.Id)
                where patient.isMalicious(numOfCanceledEventsInLastMonth)
                select patient).ToList();
        }
        private int CalculateInLastMonthForPatient(int patientId)
        {
            return _uow.GetRepository<IScheduledEventReadRepository>()
                .GetNumberOfCanceledEventsForPatient(patientId).Count(e => e.IsCanceledThisMonth());
        }
    }
}
