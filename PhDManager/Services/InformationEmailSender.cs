using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using PhDManager.Locales;
using PhDManager.Models.Options;

namespace PhDManager.Services
{
    public class InformationEmailSender(IOptions<EmailSenderOptions> emailSenderOptions, IStringLocalizer<Resources> localizer)
    {
        private readonly EmailSender _emailSender = new(emailSenderOptions);

        public Task SendThesisInterestAsync(string email, string studentName, string thesisName) =>
            _emailSender.SendEmailAsync(email, localizer["NewInterestEmail"], localizer["NewInterestEmailHtml", studentName, thesisName]);

        public Task SendInvitationAsync(string email, string invitationLink) =>
            _emailSender.SendEmailAsync(email, localizer["InvitationEmail"], localizer["InvitationEmailHtml", invitationLink]);
    }
}
