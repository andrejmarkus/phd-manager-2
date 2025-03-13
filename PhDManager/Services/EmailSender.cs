using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using PhDManager.Models.Options;

namespace PhDManager.Services
{
    public class EmailSender(IOptions<EmailSenderOptions> emailSenderOptions) : IEmailSender
    {
        private readonly EmailSenderOptions _emailSenderOptions = emailSenderOptions.Value;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string host = _emailSenderOptions.Host;
            int port = _emailSenderOptions.Port;
            string username = _emailSenderOptions.Username;
            string password = _emailSenderOptions.Password;
            string from = _emailSenderOptions.From;
            string name = _emailSenderOptions.Name;
            bool enableSsl = _emailSenderOptions.EnableSSL;

            var message = new MimeMessage();

            var sender = MailboxAddress.Parse(from);
            if (!string.IsNullOrEmpty(name))
                sender.Name = name;
            message.Sender = sender;
            message.From.Add(sender);
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

            using var smtp = new SmtpClient();
            smtp.Timeout = 10000;
            try
            {
                await smtp.ConnectAsync(host, port, enableSsl);
                if (!string.IsNullOrEmpty(username))
                    smtp.Authenticate(username, password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }
    }
}
