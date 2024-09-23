using System.Net;
using System.Net.Mail;
using UpBack.Application.Abstractions.Email;
using UpBack.Domain.ObjectValues;

namespace UpBack.Infrastructure.Email
{
    internal sealed class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        public EmailService()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("", ""),
                EnableSsl = true
            };
        }
        public async Task SendMailAsync(CustomerEmail recipient, string subject, string body)
        {
            var mailMessage = new MailMessage("your-email@gmail.com", recipient.Value)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
