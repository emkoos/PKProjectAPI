using MailKit.Net.Smtp;
using MimeKit;
using PKProject.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string mail = "pkprojectapp@gmail.com";
        private readonly string password = "Admin@123";
        private readonly string host = "smtp.gmail.com";
        private readonly int port = 587;

        public async Task<bool> SendEmail(string subject, string toUserEmail, string text)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(mail));
            message.To.Add(MailboxAddress.Parse(toUserEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = text };

            using (var client = new SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                client.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(mail, password);

                await client.SendAsync(message);
                client.Disconnect(true);
            }

            return true;
        }
    }
}
