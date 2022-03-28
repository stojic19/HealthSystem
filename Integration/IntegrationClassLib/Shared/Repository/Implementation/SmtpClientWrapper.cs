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
            _smtpClient = new SmtpClient(host)
            {
                Port = port,
                DeliveryMethod = smtpDeliveryMethod,
                UseDefaultCredentials = useDefaultCredentials,
                Credentials = credentials,
                EnableSsl = enableSsl

            };
        }
        public void Send(MailMessage mailMessage)
        {
           
            _smtpClient.Send(mailMessage);
        }
    }
}
