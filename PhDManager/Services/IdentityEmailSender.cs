using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using PhDManager.Data;
using PhDManager.Locales;
using PhDManager.Models.Options;

namespace PhDManager.Services
{
    public class IdentityEmailSender(IOptions<EmailSenderOptions> emailSenderOptions, IStringLocalizer<Resources> localizer) : IEmailSender<ApplicationUser>
    {
        private readonly EmailSender _emailSender = new(emailSenderOptions);

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
            _emailSender.SendEmailAsync(email, localizer["ConfirmYourEmail"], localizer["ConfirmYourEmailHtml", confirmationLink]);

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            _emailSender.SendEmailAsync(email, localizer["ResetYourPassword"], localizer["ResetYourPasswordCodeHtml", resetCode]);

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            _emailSender.SendEmailAsync(email, localizer["ResetYourPassword"], localizer["ResetYourPasswordLinkHtml", resetLink]);
    }
}
