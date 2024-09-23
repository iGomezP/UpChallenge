using UpBack.Domain.ObjectValues;

namespace UpBack.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task SendMailAsync(CustomerEmail recipient, string subject, string body);
    }
}
