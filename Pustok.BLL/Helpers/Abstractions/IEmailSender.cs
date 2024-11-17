namespace Pustok.BLL.Helpers.Abstractions;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string body);
}
