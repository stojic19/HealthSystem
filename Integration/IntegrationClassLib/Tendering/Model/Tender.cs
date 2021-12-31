using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Pharmacies.Model;
using Integration.Shared.Model;
using Microsoft.Extensions.Options;

namespace Integration.Tendering.Model
{
    public class Tender
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Tender()
        {
            tenderOffers = new List<TenderOffer>();
        }
        public Tender(string name, TimeRange activeRange)
        {
            MedicationRequests = new List<MedicationRequest>();
            TenderOffers = new List<TenderOffer>();
            CreatedTime = DateTime.Now;
            ClosedTime = DateTime.MinValue;
            Name = name;
            ActiveRange = activeRange;
            Validate();
        }

        private List<TenderOffer> tenderOffers;
        public List<TenderOffer> TenderOffers
        {
            get => new(tenderOffers);
            private set => tenderOffers = value;
        }

        private List<MedicationRequest> medicationRequests;
        public List<MedicationRequest> MedicationRequests
        {
            get => new(medicationRequests);
            private set => medicationRequests = value;
        }
        public DateTime CreatedTime { get; private set; }
        public DateTime ClosedTime { get; private set; }
        public TimeRange ActiveRange { get; private set; }
        public int? WinningOfferId { get; private set; }
        public TenderOffer WinningOffer { get; private set; }

        private void Validate()
        {
            if (Name.Length < 1) throw new ArgumentException("Invalid tender name");
            if (ClosedTime != DateTime.MinValue && ClosedTime < CreatedTime)
                throw new ArgumentException("Date of closure is less than date of creation");
        }

        public void ChooseWinner(TenderOffer tenderOffer)
        {
            foreach (TenderOffer iterationOffer in TenderOffers)
            {
                if (tenderOffer.Id == iterationOffer.Id)
                {
                    iterationOffer.MarkAsWinning();
                    WinningOffer = iterationOffer;
                    WinningOfferId = WinningOffer.Id;
                    return;
                }
            }
        }

        public int NumberOfPharmacyOffers(Pharmacy pharmacy)
        {
            int offerNumber = 0;
            foreach (TenderOffer iterationOffer in TenderOffers)
            {
                if (iterationOffer.IsThisPharmacy(pharmacy)) offerNumber++;
            }

            return offerNumber;
        }

        public bool DidPharmacyWin(Pharmacy pharmacy)
        {
            if (WinningOffer == null) return false;
            return WinningOffer.IsThisPharmacy(pharmacy);
        }

        public bool WasMedicationNeeded(string medicationName)
        {
            foreach (MedicationRequest iterationMedicationRequest in MedicationRequests)
            {
                if (iterationMedicationRequest.CheckName(medicationName)) return true;
            }

            return false;
        }

        public void CloseTender()
        {
            ClosedTime = DateTime.Now;
            Validate();
        }

        public bool IsActive()
        {
            return ClosedTime == DateTime.MinValue && ActiveRange.Includes(DateTime.Now);
        }

        public void AddMedicationRequest(MedicationRequest request)
        {
            medicationRequests.Add(request);
        }

        public void AddTenderOffer(TenderOffer offer)
        {
            tenderOffers.Add(offer);
        }
    }
}
