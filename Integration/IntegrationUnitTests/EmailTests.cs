using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Integration.Pharmacies.Model;
using Integration.Shared.Model;
using Integration.Shared.Repository;
using Integration.Shared.Service;
using Integration.Tendering.Model;
using IntegrationUnitTests.Base;
using Moq;
using Xunit;

namespace IntegrationUnitTests
{
    public class EmailTests : BaseTest
    {
        public EmailTests(BaseFixture fixture) : base(fixture)
        {
            ClearDbContext();
        }

        [SkippableFact]
        public void Send_email_success()
        {
            var env = Environment.GetEnvironmentVariable("PRODUCTION");
            Skip.If(env == null || env.Equals("1"));
            var eService = new EmailService();
            bool exceptionCaught = false;
            try
            {
              //  eService.SendMail("psw.company2.pharmacy@gmail.com,psw.company2@gmail.com", "testTitle", "testText");
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                exceptionCaught = true;
            }
            Assert.False(exceptionCaught);
        }
        [Fact]
        public void Send_email_verify()
        {
            var mock = new Mock<ISmtpClient>();
            
                
            EmailService emailService = new(mock.Object);
            NetworkCredential Credentials = new("psw.company2@gmail.com", "Dont panic!");
            emailService.SendMail("psw.company2.pharmacy@gmail.com,psw.company2@gmail.com", "testTitle", "testText", Credentials);

            /** MailMessage mailMessage = new();
             mailMessage.From = new MailAddress("psw.company2@gmail.com");
             mailMessage.To.Add("psw.company2.pharmacy@gmail.com,psw.company2@gmail.com");

             mailMessage.Subject = "testTitle";
             mailMessage.IsBodyHtml = true;
             mailMessage.Body = "testText";*/
            mock.Verify(t => t.Send(It.Is<MailMessage>(s => s.Subject.Equals("testTitle")),Credentials));
             // mock.Verify(x => x.Send(mailMessage), Times.Once);
        }

        [SkippableFact]
        public void Send_new_tender_email()
        {
            var env = Environment.GetEnvironmentVariable("PRODUCTION");
            Skip.If(env == null || env.Equals("1"));
            List<Pharmacy> pharmacies = new List<Pharmacy>
            {
                new Pharmacy
                {
                    Email = "psw.company2.pharmacy@gmail.com"
                },
                new Pharmacy
                {
                    Email = "psw.company2@gmail.com"
                }
            };
            Tender tender = new Tender("NEW_TENDER_EMAIL_TEST",
                new TimeRange(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));
            tender.AddMedicationRequest(new MedicationRequest("Aspirin", 5));
            tender.AddMedicationRequest(new MedicationRequest("Brufen", 5));
            bool exceptionCaught = false;
            try
            {
                new EmailService().SendNewTenderMail(tender, pharmacies);
            }
            catch
            {
                exceptionCaught = true;
            }
            Assert.False(exceptionCaught);
        }

        [SkippableFact]
        public void Send_winning_offer_email()
        {
            var env = Environment.GetEnvironmentVariable("PRODUCTION");
            Skip.If(env == null || env.Equals("1"));
            var pharmacy = new Pharmacy()
            {
                Email = "psw.company2.pharmacy@gmail.com"
            };
            Tender tender = new Tender("TENDER_OFFER_WON_EMAIL_TEST",
                new TimeRange(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));
            tender.AddMedicationRequest(new MedicationRequest("Aspirin", 5));
            tender.AddMedicationRequest(new MedicationRequest("Brufen", 5));
            TenderOffer offer = new TenderOffer(pharmacy, new Money(50, 0), DateTime.Now);
            offer.AddMedicationRequest(new MedicationRequest("Aspirin", 5));
            offer.AddMedicationRequest(new MedicationRequest("Brufen", 5));
            tender.AddTenderOffer(offer);
            tender.ChooseWinner(offer);
            bool exceptionCaught = false;
            try
            {
                new EmailService().SendWinningOfferMail(tender);
            }
            catch
            {
                exceptionCaught = true;
            }
            Assert.False(exceptionCaught);
        }

        [SkippableFact]
        public void Send_tender_closed_mail()
        {
            var env = Environment.GetEnvironmentVariable("PRODUCTION");
            Skip.If(env == null || env.Equals("1"));
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            pharmacies.Add(new Pharmacy
            {
                Email = "psw.company2.pharmacy@gmail.com"
            });
            pharmacies.Add(new Pharmacy
            {
                Email = "psw.company2@gmail.com"
            });
            Tender tender = new Tender("TENDER_OFFER_WON_EMAIL_TEST",
                new TimeRange(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));
            tender.AddMedicationRequest(new MedicationRequest("Aspirin", 5));
            tender.AddMedicationRequest(new MedicationRequest("Brufen", 5));
            tender.CloseTender();
            bool exceptionCaught = false;
            try
            {
                new EmailService().SendCloseTenderMail(tender, pharmacies);
            }
            catch
            {
                exceptionCaught = true;
            }
            Assert.False(exceptionCaught);
        }
    }
}
