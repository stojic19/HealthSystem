using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.DTO
{
    public class CreateComplaintDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PharmacyName { get; set; }
        public int PharmacyId { get; set; }
        public CreateComplaintDTO() { }
    }
}
