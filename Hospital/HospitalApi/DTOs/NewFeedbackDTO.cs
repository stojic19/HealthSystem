namespace HospitalApi.DTOs
{
    public class NewFeedbackDTO
    {
        public int PatientId { get; }
        public string Text { get; }
        public bool IsPublishable { get; }
        public bool IsAnonymous { get;  }
    }
}
