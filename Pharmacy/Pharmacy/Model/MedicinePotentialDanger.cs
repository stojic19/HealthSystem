using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class MedicinePotentialDanger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Medicine> Medicines { get; set; }
    }
}
