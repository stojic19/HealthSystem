using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Model
{
    public class Benefit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public bool Published { get; set; }

    }
}
