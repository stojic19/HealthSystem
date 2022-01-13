using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository.Base;
using Integration.Tendering.Model;

namespace Integration.Shared.Service
{
    public class EmailService
    {
        private readonly IUnitOfWork _uow;
        private readonly string _hospitalName = "Nasa bolnica";
        private readonly string _hospitalEmail = "psw.company2@gmail.com";
        private readonly string _emailPassword = "Dont panic!";
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

        public void SendNewTenderMail(Tender tender, IEnumerable<Pharmacy> pharmacies)
        {
            var mail = PrepareMailNames(pharmacies);
            string text = "<p>Greetings</p><p>We have created a new tender named " + tender.Name
                + ". This tender requires following medications:";
            text = MakeMedicationList(tender, text);
            text += "</p>";
            text = MakeRegardsText(text);
            SendMail(mail, "New Tender", text);
        }

        private string MakeRegardsText(string text)
        {
            text += "<p>Best regards,</p><p>" + _hospitalName + "</p>";
            return text;
        }

        private string MakeMedicationList(Tender tender, string text)
        {
            text += "<ul>";
            foreach (var medReq in tender.MedicationRequests)
            {
                text += "<li>" + medReq.MedicineName + " Amount: " + medReq.Quantity + "</li>";
            }

            text += "</ul>";
            return text;
        }

        private string PrepareMailNames(IEnumerable<Pharmacy> pharmacies)
        {
            string mail = "";
            foreach (string pharmacyMail in pharmacies.Select(x => x.Email))
            {
                mail += pharmacyMail;
                mail += ",";
            }

            mail = mail.Remove(mail.Length - 1);
            return mail;
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
