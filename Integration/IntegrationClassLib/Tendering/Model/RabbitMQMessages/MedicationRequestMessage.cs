using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Tendering.Model.RabbitMQMessages
{
    public class MedicationRequestMessage
    {
        [Required(ErrorMessage = "Medicine name is needed")]
        public string MedicineName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Must be positive!")]
        public int Quantity { get; set; }
    }
}
