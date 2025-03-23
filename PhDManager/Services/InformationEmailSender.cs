using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using PhDManager.Locales;
using PhDManager.Models.Options;

namespace PhDManager.Services
{
    public class InformationEmailSender(IOptions<EmailSenderOptions> emailSenderOptions, IStringLocalizer<Resources> localizer)
    {
        private readonly IEmailSender emailSender = new EmailSender(emailSenderOptions);

        public Task SendThesisInterestAsync(string email, string studentName, string thesisName) =>
            emailSender.SendEmailAsync(email, localizer["NewInterestEmail"], localizer["NewInterestEmailHtml", studentName, thesisName]);
    }
}
