using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using ZdravoHospital.GUI.PatientUI.DTOs;

namespace ZdravoHospital.GUI.PatientUI.Services.Strategy
{
    public class SuggestDateService : SuggestAbstract, ISuggestStrategy
    {
        public string DoctorsUsername { get; private set; }
        public string PatientUsername { get; set; }
        public SuggestDateService(ObservableCollection<PeriodDTO> suggestedPeriods,string doctorUsername, string patientUsername):base(suggestedPeriods)
        {
            DoctorsUsername = doctorUsername;
            PatientUsername = patientUsername;
        }
        public void Suggest()
        {
            GetSuggestedPeriods();
        }

        private void GetSuggestedPeriods()
        {
            int daysFromToday = 3;
            while (SuggestedPeriods.Count < 2)
            {
                SuggestedPeriods.Clear();
                AddFreeTimes(daysFromToday, PatientUsername);
                daysFromToday++;
            }
        }


        private void AddFreeTimes(int daysFromToday, string patientUsername)
        {
            List<TimeSpan> timeList = new List<TimeSpan>();
            Injection.GenerateTimeSpan(timeList);
            foreach (TimeSpan timeSpan in timeList) if (SuggestedPeriods.Count < 4)
                {
                    Period period = GeneratePeriod(timeSpan, daysFromToday, patientUsername);
                    if (PeriodService.CheckPeriodAvailability(period) && period.RoomId != -1)
                        SuggestedPeriods.Add(PeriodConverter.GetPeriodDTO(period));
                }
        }

        private Period GeneratePeriod(TimeSpan timeSpan, int daysFromToday, string patientUsername)
        {
            Period period = new Period(DateTime.Today.AddDays(daysFromToday), 30, PeriodType.APPOINTMENT,
                patientUsername, DoctorsUsername, false, PeriodService.GeneratePeriodId());
            period.StartTime += timeSpan;
       
            period.RoomId = RoomScheduleService.GetFreeRoom(period);
            return period;
        }

    }
}
