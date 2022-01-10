using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository;
using HospitalIntegrationTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIntegrationTests
{
    public class ManagingOnCallShiftsTests : BaseTest
    {
        public ManagingOnCallShiftsTests(BaseFixture fixture) : base(fixture)
        {
        }

        private Doctor InsertDoctor(string username)
        {
            var doctor = UoW.GetRepository<IDoctorReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.UserName == username);

            if (doctor == null)
            {
                doctor = new Doctor()
                {
                   
                };

                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            return doctor;
        }

        private OnCallDuty InsertOnCallShift(int month, int week)
        {
            var shift = UoW.GetRepository<IOnCallDutyReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Month == month && x.Week == week);

            if (shift == null)
            {
                shift = new OnCallDuty(month, week, new List<Doctor>());

                UoW.GetRepository<IOnCallDutyWriteRepository>().Add(shift);
            }

            return shift;
        }
    }
}
