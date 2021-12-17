using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
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
            var patient = _uow.GetRepository<IPatientReadRepository>().GetAll().FirstOrDefault(x => x.UserName.Equals(userName));
            if (patient == null) return;
            patient.IsBlocked = true;
            _uow.GetRepository<IPatientWriteRepository>().Update(patient);
        }

        public List<Patient> GetMaliciousPatients()
        {
            var patients = _uow.GetRepository<IPatientReadRepository>().GetAll().Where(x => x.IsBlocked == false).ToList();
            var malicious = new List<Patient>();
            foreach (var patient in patients)
            {
                var canceledEvents = _uow.GetRepository<IScheduledEventReadRepository>()
                    .GetNumberOfCanceledEventsForPatient(patient.Id);
                var numOfCanceledEventsInLastMonth = calculateInLastMonth(canceledEvents);
                if (numOfCanceledEventsInLastMonth >= 3)
                {
                    malicious.Add(patient);
                }
            }
            return malicious;
        }
        private int calculateInLastMonth(List<ScheduledEvent> canceledEvents)
        {
            return canceledEvents.Count(e => e.CancellationDate > DateTime.Now.AddDays(-30));
        }
    }
}
