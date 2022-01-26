using Hospital.Schedule.Model;

namespace HospitalApi.DTOs
{
    public class DoctorShiftDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Shift Shift { get; set; }

    }
}
