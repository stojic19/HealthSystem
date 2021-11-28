using System;
using Hospital.Medical_records.Model;
using Hospital.Rooms_and_equipment.Model;
using Hospital.Shared_model.Model;
using Hospital.Shared_model.Model.Enumerations;

namespace Hospital.Schedule.Model
{
    public class ScheduledEvent
    {
        public int Id { get; set; }

        public ScheduledEventType ScheduledEventType { get; set; }

        public bool IsCanceled { get; set; }
        public bool IsDone { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        
        public Patient Patient { get; set; }
        
        public Doctor Doctor { get; set; }

        public int? RoomId { get; set; }
        public Room Room { get; set; }

        public int AnsweredSurveyId { get; set; }
        public AnsweredSurvey AnsweredSurvey { get; set; }
    }
}
