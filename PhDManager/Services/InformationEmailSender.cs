using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using PhDManager.Locales;
using PhDManager.Models.Options;

namespace PhDManager.Services
{
    public class InformationEmailSender(IOptions<EmailSenderOptions> emailSenderOptions, IStringLocalizer<Resources> localizer)
    {
        private readonly EmailSender emailSender = new(emailSenderOptions);

        public Task SendThesisInterestAsync(string email, string studentName, string thesisName) =>
            emailSender.SendEmailAsync(email, localizer["NewInterestEmail"], localizer["NewInterestEmailHtml", studentName, thesisName]);

        public Task SendInvitationAsync(string email, string invitationLink) =>
            emailSender.SendEmailAsync(email, localizer["InvitationEmail"], localizer["InvitationEmailHtml", invitationLink]);
    }
}
