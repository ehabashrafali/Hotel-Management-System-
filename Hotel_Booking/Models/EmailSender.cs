using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Hotel_Booking.Models
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //var fromAddress = new MailAddress(_configuration["SmtpSettings:Email"], "Your Name");
            //var toAddress = new MailAddress(email);
            //var fromPassword = _configuration["SmtpSettings:Password"];
            //string body = htmlMessage;

            //var smtp = new SmtpClient
            //{
            //    Host = _configuration["SmtpSettings:Host"],
            //    Port = int.Parse(_configuration["SmtpSettings:Port"]),
            //    EnableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]),
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            //};

            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body,
            //    IsBodyHtml = true
            //})
            //{
            //    await smtp.SendMailAsync(message);
            //}

            return Task.CompletedTask;
        }
    }
}
