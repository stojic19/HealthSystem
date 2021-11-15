using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.DTO
{
    public class ComplaintDTO
    {
        public int ComplaintId { get; set; }
        public string ApiKey { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
