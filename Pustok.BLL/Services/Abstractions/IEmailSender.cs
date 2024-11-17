namespace Pustok.BLL.Services.Abstractions;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string body);
}
