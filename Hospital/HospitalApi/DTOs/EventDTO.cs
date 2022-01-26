using Hospital.EventStoring.Model.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTOs
{
    public class EventDTO
    {
        [Required(ErrorMessage = "User is mandatory")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Step is mandatory")]
        public Step Step { get; set; }
    }
}
