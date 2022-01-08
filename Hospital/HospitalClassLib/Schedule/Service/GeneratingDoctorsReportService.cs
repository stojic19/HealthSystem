using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Service
{
    public class GeneratingDoctorsReportService
    {
        private readonly IUnitOfWork uow;

        public GeneratingDoctorsReportService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DoctorsScheduleReport GetReportInformation(TimePeriod timePeriod, int doctorsId)
        {
            List<ScheduledEvent> foundAppointments = CountAppointmentsForTimePeriod(timePeriod, doctorsId);
            int numOfPatients = CountPatients(foundAppointments);
            int numOfOnCallDuties = CountOnCallDuties(doctorsId);

            DoctorsScheduleReport doctorsReport =
                new DoctorsScheduleReport(foundAppointments.Count(), numOfOnCallDuties, numOfPatients, timePeriod.StartTime, timePeriod.EndTime);

            return doctorsReport;
        }

        private List<ScheduledEvent> CountAppointmentsForTimePeriod(TimePeriod timePeriod, int doctorsId)
        {
            var scheduledEventRepo = uow.GetRepository<IScheduledEventReadRepository>();
            var appointmentsForDoctor = scheduledEventRepo.GetAll()
                                            .Where(s => s.DoctorId == doctorsId);

            List<ScheduledEvent> foundAppointments = new List<ScheduledEvent>();
            foreach (ScheduledEvent appointment in appointmentsForDoctor)
            {
                var scheduledEventPeriod = new TimePeriod(appointment.StartDate, appointment.EndDate);
                if (timePeriod.OverlapsWith(scheduledEventPeriod))
                {
                    foundAppointments.Add(appointment);
                }
            }

            return foundAppointments;
        }

        private int CountPatients(List<ScheduledEvent> events)
        {
            List<int> AllPatients = new List<int>();
            foreach (ScheduledEvent appointment in events)
            {
                AllPatients.Add(appointment.PatientId);
            }

            return AllPatients.Distinct().Count();
        }

        private int CountOnCallDuties(int doctorsId)
        {
            var doctor = uow.GetRepository<IDoctorReadRepository>()
                .GetById(doctorsId);

            var duties = uow.GetRepository<IOnCallDutyReadRepository>()
                .GetAll()
                .Where(d => d.DoctorsOnDuty.Contains(doctor));

            return duties.Count();
        }
    }
}
