using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Pharmacies.Model;
using Integration.Shared.Model;
using Integration.Shared.Service;
using Integration.Tendering.Model;
using IntegrationUnitTests.Base;
using Xunit;

namespace IntegrationUnitTests
{
    public class EmailTests : BaseTest
    {
        public EmailTests(BaseFixture fixture) : base(fixture)
        {
            ClearDbContext();
        }

        [Fact]
        public void Send_email_success()
        {
            var eService = new EmailService(UoW);
            bool exceptionCaught = false;
            try
            {
                eService.SendMail("radisaaca@gmail.com,podmilance@gmail.com", "testTitle", "testText");
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                exceptionCaught = true;
            }
            Assert.False(exceptionCaught);
        }

        [Fact]
        public void Send_new_tender_email()
        {
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            pharmacies.Add(new Pharmacy
            {
                Email = "psw.company2.pharmacy@gmail.com"
            });
            pharmacies.Add(new Pharmacy
            {
                Email = "radisaaca@gmail.com"
            });
            Tender tender = new Tender("NEW_TENDER_EMAIL_TEST",
                new TimeRange(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));
            tender.AddMedicationRequest(new MedicationRequest("Aspirin", 5));
            tender.AddMedicationRequest(new MedicationRequest("Brufen", 5));
            bool exceptionCaught = false;
            try
            {
                new EmailService(UoW).SendNewTenderMail(tender, pharmacies);
            }
            catch
            {
                exceptionCaught = true;
            }
            Assert.False(exceptionCaught);
        }
    }
}
