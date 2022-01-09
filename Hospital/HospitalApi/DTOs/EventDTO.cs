using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class EventDTO
    {
        [Required(ErrorMessage = "User is mandatory")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "State data is mandatory")]
        public string StateData { get; set; }
    }
}
