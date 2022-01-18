﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Model;
using Pharmacy.Services;
using PharmacyUnitTests.Base;
using Xunit;

namespace PharmacyUnitTests
{
    public class EmailTests : BaseTest
    {
        public EmailTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Send_new_tender_offer_email_success()
        {
            Hospital hospital = new Hospital
            {
                Email = "psw.company2@gmail.com"
            };
            Tender tender = new Tender
            {
                Name = "EMAIL_TENDER_TEST",
                Hospital = hospital
            };
            List<MedicationRequest> medReqs = new List<MedicationRequest>();
            medReqs.Add(new MedicationRequest("Aspirin", 5));
            medReqs.Add(new MedicationRequest("Brufen", 5));
            TenderOffer tenderOffer = new TenderOffer()
            {
                Tender = tender,
                MedicationRequests = medReqs
            };
            bool exceptionCaught = false;
            try
            {
                new EmailService().SendNewTenderOfferMail(tenderOffer);
            }
            catch
            {
                exceptionCaught = true;
            }
            Assert.False(exceptionCaught);
        }

        [Fact]
        public void Send_tender_offer_confirmed_email_success()
        {
            Hospital hospital = new Hospital
            {
                Email = "psw.company2@gmail.com"
            };
            Tender tender = new Tender
            {
                Name = "EMAIL_TENDER_TEST",
                Hospital = hospital
            };
            List<MedicationRequest> medReqs = new List<MedicationRequest>();
            medReqs.Add(new MedicationRequest("Aspirin", 5));
            medReqs.Add(new MedicationRequest("Brufen", 5));
            TenderOffer tenderOffer = new TenderOffer()
            {
                Tender = tender,
                MedicationRequests = medReqs
            };
            bool exceptionCaught = false;
            try
            {
                new EmailService().SendTenderOfferConfirmationMail(tenderOffer);
            }
            catch
            {
                exceptionCaught = true;
            }
            Assert.False(exceptionCaught);
        }
    }
}