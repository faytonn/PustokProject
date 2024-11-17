using Microsoft.Extensions.Configuration;
using Pustok.BLL.Services.Abstractions;
using System.Net;
using System.Net.Mail;

namespace Pustok.BLL.Services.Implementations;

public class EmailSenderService : IEmailSender
{
    private readonly IConfiguration _configuration;
    public EmailSenderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task SendEmailAsync(string email, string subject, string body)
    {
        var client = new SmtpClient("fehledehle@gmail.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("faytonn7@gmail.com", "Lachcool41")
        };


        return client.SendMailAsync(new MailMessage(from:
            "faytonn7@gmail.com",
            to: email,
            subject,
            body));
    }
}
