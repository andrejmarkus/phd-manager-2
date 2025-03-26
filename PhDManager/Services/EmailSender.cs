using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using PhDManager.Models.Options;

namespace PhDManager.Services
{
    public class EmailSender(IOptions<EmailSenderOptions> emailSenderOptions) : IEmailSender
    {
        private readonly EmailSenderOptions _emailSenderOptions = emailSenderOptions.Value;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();

            try
            {
                var sender = MailboxAddress.Parse(_emailSenderOptions.From);
                if (!string.IsNullOrEmpty(_emailSenderOptions.Name))
                    sender.Name = _emailSenderOptions.Name;

                message.Sender = sender;
                message.From.Add(sender);
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;
                message.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating email: {ex.Message}", ex);
            }

            using var client = new SmtpClient();
            client.Timeout = 10000;

            try
            {
                await client.ConnectAsync(_emailSenderOptions.Host, _emailSenderOptions.Port, _emailSenderOptions.EnableSSL);

                if (!string.IsNullOrEmpty(_emailSenderOptions.Username))
                    await client.AuthenticateAsync(_emailSenderOptions.Username, _emailSenderOptions.Password);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (SmtpCommandException ex)
            {
                throw new SmtpCommandException(ex.ErrorCode, ex.StatusCode, $"SMTP Command Error: {ex.Message}", ex);
            }
            catch (SmtpProtocolException ex)
            {
                throw new SmtpProtocolException($"SMTP Protocol Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error sending email: {ex.Message}", ex);
            }
        }
    }
}
