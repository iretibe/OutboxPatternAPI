using Microsoft.Extensions.Options;
using OutboxPatternAPI.Helper;
using System.Net;
using System.Net.Mail;

namespace OutboxPatternAPI.Services
{
    public class MailService : IMailService
    {
        private readonly EmailSettings _emailSettings;

        public MailService(IOptions<EmailSettings> emailOptions)
        {
            _emailSettings = emailOptions.Value;
        }

        public bool Send(string sender, string subject, string body, bool isBodyHTML)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(_emailSettings.Email);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isBodyHTML;

                mailMessage.To.Add(new MailAddress(sender));

                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";

                smtp.EnableSsl = false;

                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = mailMessage.From.Address;
                networkCredential.Password = _emailSettings.Password;

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCredential;

                smtp.Port = 587;

                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
