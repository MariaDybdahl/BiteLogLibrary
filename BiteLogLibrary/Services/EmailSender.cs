using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BiteLogLibrary.Services
{
    public class EmailSender
    {
        private readonly IConfiguration _cfg;
        public EmailSender(IConfiguration cfg) => _cfg = cfg;

        public async Task SendAsync(string toEmail, string subject, string textBody, string? htmlBody = null)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("BiteLog", _cfg["Smtp:From"]));
            msg.To.Add(MailboxAddress.Parse(toEmail));
            msg.Subject = subject;

            if (htmlBody is null)
                msg.Body = new TextPart("plain") { Text = textBody };
            else
                msg.Body = new BodyBuilder { TextBody = textBody, HtmlBody = htmlBody }.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_cfg["Smtp:Host"], int.Parse(_cfg["Smtp:Port"]), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_cfg["Smtp:User"], _cfg["Smtp:Password"]);
            await smtp.SendAsync(msg);
            await smtp.DisconnectAsync(true);
        }
    }
}
