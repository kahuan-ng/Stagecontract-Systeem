using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;
using WSI.Goat.InternshipsContracts.Web.Services.Interfaces;

namespace WSI.Goat.InternshipsContracts.Web.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "STool.mailservice@gmail.com";
            var pw = "xtsezeeqnvnzrizy";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(
                    from: mail,
                    to: email,
                    subject: subject,
                    body: message
                )
                {
                    IsBodyHtml = true
                });
        }
    }
}
