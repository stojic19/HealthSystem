using System.Collections.ObjectModel;
using ZdravoHospital.GUI.PatientUI.Converters;
using ZdravoHospital.GUI.PatientUI.DTOs;
using ZdravoHospital.GUI.PatientUI.Logics;

namespace ZdravoHospital.GUI.PatientUI.Services.Strategy
{
    public abstract class SuggestAbstract
    {
        public ObservableCollection<PeriodDTO> SuggestedPeriods { get; private set; }
        public InjectService Injection { get; private set; }
        public PeriodConverter PeriodConverter { get; private set; }
        public PeriodService PeriodService { get; set; }
        public RoomSheduleService RoomScheduleService { get; private set; }
        public string PatientUsername { get; set; }

        public SuggestAbstract(ObservableCollection<PeriodDTO> suggestedPeriods, string patientUsername)
        {
            PatientUsername = patientUsername;
            SuggestedPeriods = suggestedPeriods;
            Injection = new InjectService();
            PeriodConverter = new PeriodConverter(patientUsername);
            PeriodService = new PeriodService(patientUsername);
            RoomScheduleService = new RoomSheduleService(patientUsername);
        }
    }
}
