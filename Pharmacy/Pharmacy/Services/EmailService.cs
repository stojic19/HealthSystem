using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Services
{
    public class EmailService
    {
        private readonly IUnitOfWork _uow;
        private readonly string _pharmacyName = "Psw2Pharmacy";
        private readonly string _hospitalEmail = "psw.company2.pharmacy@gmail.com";
        private readonly string _emailPassword = "SamoSeViIgrajte21@!";
        public EmailService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public EmailService(IUnitOfWork unitOfWork, string hospitalEmail, string password)
        {
            _uow = unitOfWork;
            _hospitalEmail = hospitalEmail;
            _emailPassword = password;
        }


        public void SendNewTenderOfferMail(TenderOffer tenderOffer)
        {
            string text = "<p>Greetings</p><p>We have sent our tender offer for your tender named " +
                          tenderOffer.Tender.Name + ". This offer contains:<ul>";
            text = MakeMedicationList(tenderOffer, text);

            text = MakeRegardsText(text);
            SendMail(tenderOffer.Tender.Hospital.Email, "New Tender Offer", text);
        }

        private string MakeRegardsText(string text)
        {
            text += "</ul></p><p>Best regards,</p><p>" + _pharmacyName + "</p>";
            return text;
        }

        private string MakeMedicationList(TenderOffer tenderOffer, string text)
        {
            foreach (var medReq in tenderOffer.MedicationRequests)
            {
                text += "<li>" + medReq.MedicineName + " Amount: " + medReq.Quantity + "</li>";
            }

            return text;
        }

        public void SendTenderOfferConfirmationMail(TenderOffer tenderOffer)
        {
            string text = "<p>Greetings</p><p>We have sent medication we promised in our tender offer for tender named " +
                          tenderOffer.Tender.Name + ". These medications include:<ul>";
            text = MakeMedicationList(tenderOffer, text);

            text = MakeRegardsText(text);
            SendMail(tenderOffer.Tender.Hospital.Email, "Medication Sent", text);
        }

        public void SendMail(string mail, string title, string text)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_hospitalEmail);
            mailMessage.To.Add(mail);

            mailMessage.Subject = title;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = text;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(_hospitalEmail, _emailPassword)
            };
            smtpClient.Send(mailMessage);
        }
    }
}
