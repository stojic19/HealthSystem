using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class MedicationRequest
    {
        public string MedicineName { get; private set; }
        public int Quantity { get; private set; }
        private MedicationRequest() { }
        public MedicationRequest(string medicineName, int quantity)
        {
            MedicineName = medicineName;
            Quantity = quantity;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(MedicineName)) throw new ArgumentException("Invalid medicine name");
            if (Quantity < 1) throw new ArgumentException("Invalid quantity");
        }

        public bool CheckName(string name)
        {
            return MedicineName.ToLower().Equals(name.ToLower());
        }
        public void Add(int amount)
        {
            Quantity += amount;
            Validate();
        }
        public void Remove(int amount)
        {
            Quantity -= amount;
            Validate();
        }
    }
}
