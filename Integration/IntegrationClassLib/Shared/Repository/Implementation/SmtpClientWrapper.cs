using System.Net;
using System.Net.Mail;

namespace Integration.Shared.Repository.Implementation
{
    public class SmtpClientWrapper : ISmtpClient
    {
        private readonly SmtpClient _smtpClient;

        public SmtpClientWrapper(string host, int port, SmtpDeliveryMethod smtpDeliveryMethod, bool useDefaultCredentials
            , bool enableSsl, NetworkCredential credentials)
        {
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true

            };
        }
        public void Send(MailMessage mailMessage)
        {
           
            _smtpClient.Send(mailMessage);
        }

        public void Send(MailMessage mailMessage, NetworkCredential networkCredential)
        {
            _smtpClient.Credentials = networkCredential;
            _smtpClient.Send(mailMessage);
        }
    }
}
