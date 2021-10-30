using Model;
using Repository.RoomSchedulePersistance;
using System.Collections.Generic;
using System.Linq;

namespace ZdravoHospital.GUI.PatientUI.Logics
{
    public class RoomSheduleService
    {
        #region Properties
        List<Room> Rooms { get; set; }
        public RoomService RoomFunctions { get; private set; }
        public PeriodService PeriodFunctions { get; private set; }
        public string PatientUsername { get; set; }
        #endregion

        public RoomSheduleService(string patientUsername)
        {
            SetProperties(patientUsername);
        }

        private void SetProperties(string patientUsername)
        {

            PeriodFunctions = new PeriodService(patientUsername);
            RoomFunctions = new RoomService();
            Rooms = RoomFunctions.GetAll();
            PatientUsername = patientUsername;
        }

        public int GetFreeRoom(Period checkedPeriod)//vraca prvi slobodan Appointment room za zadati termin
        {
            foreach (var room in Rooms.Where(room => GetFreeRoomId(room, checkedPeriod) != -1))
                return room.Id;

            return -1;
        }

        private int GetFreeRoomId(Room room, Period checkedPeriod)
        {
            int roomId = -1;

            if (!room.IsAppointmentRoom() || !IsRoomAvailableForGivenPeriod(room, checkedPeriod)) return roomId;
            return !PeriodAlreadyExistsInRoom(room, checkedPeriod) ? room.Id : roomId;
        }
        
        private bool IsRoomAvailableForGivenPeriod(Room room, Period checkedPeriod)
        {
            RoomScheduleRepository roomScheduleRepository = new RoomScheduleRepository();
            List<RoomSchedule> roomSchedules = roomScheduleRepository.GetValues();
            return roomSchedules.All(roomSchedule => !roomSchedule.RoomId.Equals(room.Id) || !PeriodFunctions.DoPeriodsOverlap(GeneratePeriodFromSchedule(roomSchedule), checkedPeriod));
        }

        private Period GeneratePeriodFromSchedule(RoomSchedule roomSchedule)
        {
            Period period = new Period
            {
                StartTime = roomSchedule.StartTime,
                Duration = (int)roomSchedule.EndTime.Subtract(roomSchedule.StartTime).TotalMinutes,
                RoomId = -1
            };
            return period;
        }

        private bool PeriodAlreadyExistsInRoom(Room room, Period checkedPeriod)
        {

            PeriodService periodFunctions = new PeriodService(PatientUsername);
            List<Period> periods = periodFunctions.GetAllPeriods();
            return periods.Any(period => period.RoomId == room.Id && PeriodFunctions.DoPeriodsOverlap(period, checkedPeriod));
        }
    }
}
