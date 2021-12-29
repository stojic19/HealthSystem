using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Pharmacies.Model;

namespace Integration.Tendering.Model
{
    public class TenderOffer
    {
        public int Id { get; private set; }
        public int PharmacyId { get; private set; }
        public Pharmacy Pharmacy { get; private set; }

        private List<MedicationRequest> medicationRequests;
        public List<MedicationRequest> MedicationRequests
        {
            get => new(medicationRequests);
            private set => medicationRequests = value;
        }
        public DateTime CreatedDate { get; private set; }
        public Money Cost { get; private set; }
        public bool IsWinning { get; private set; }
        private TenderOffer(){}
        public TenderOffer(Pharmacy pharmacy, Money money, DateTime createdDate)
        {
            Cost = money;
            CreatedDate = createdDate;
            Pharmacy = pharmacy;
            PharmacyId = pharmacy.Id;
            MedicationRequests = new List<MedicationRequest>();
            Validate();
        }

        private void Validate()
        {
            if (CreatedDate >= DateTime.Now) throw new ArgumentException("Invalid date of creation");
        }

        public bool IsThisPharmacy(Pharmacy pharmacy)
        {
            return Pharmacy.isEqual(pharmacy);
        }

        public void MarkAsWinning()
        {
            if (IsWinning) return;
            IsWinning = true;
        }

        public void AddMedicationRequest(MedicationRequest request)
        {
            medicationRequests.Add(request);
        }
    }
}
