namespace Hospital.Model
{
    public class Patient : User
    {
        public MedicalRecord MedicalRecord { get; set; }
        public bool IsBlocked { get; set; }
    }
}
