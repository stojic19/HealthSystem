using Model;
using ZdravoHospital.GUI.PatientUI.Logics;
using PeriodDTO = ZdravoHospital.GUI.PatientUI.DTOs.PeriodDTO;

namespace ZdravoHospital.GUI.PatientUI.Converters
{
    public class PeriodConverter
    {
        private DoctorService doctorFunctions;
        private PeriodService periodFunctions;
        public string PatientUsername { get; set; }

        public PeriodConverter(string patientUsername)
        {
            PatientUsername = patientUsername;
            doctorFunctions = new DoctorService();
            periodFunctions = new PeriodService(patientUsername);
        }
        public Period GetPeriod(PeriodDTO period)
        {
            return periodFunctions.GetPeriod(period.PeriodId);
        }
        public PeriodDTO GetPeriodDTO(Period period)
        {
            Doctor doctor = doctorFunctions.GetDoctor(period.DoctorUsername);//GetDoctor(period.DoctorUsername);
            return new PeriodDTO(doctor.Name, doctor.Surname, period.StartTime, period.RoomId, period.PeriodType,
                period.PeriodId);
        }

        public Period GeneratePeriod(PeriodDTO periodDTO)
        {
            Period period = new Period
            {
                PatientUsername = PatientUsername,
                Duration = 30,
                PeriodId = periodDTO.PeriodId,
                StartTime = periodDTO.Date,
                PeriodType = periodDTO.PeriodType,
                DoctorUsername = doctorFunctions.GetDoctorUsername(periodDTO.DoctorName,periodDTO.DoctorSurname),
                RoomId = periodDTO.RoomNumber

            };
            return period;
        }

   

    }
}
