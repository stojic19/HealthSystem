using System.Net.Mail;

namespace Integration.Shared.Repository
{
    public interface ISmtpClient 
    {
        void Send(MailMessage mailMessage, System.Net.NetworkCredential networkCredential);
    }
}
