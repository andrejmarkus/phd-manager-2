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
        private readonly EmailSender emailSender = new(emailSenderOptions);

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
            emailSender.SendEmailAsync(email, localizer["ConfirmYourEmail"], localizer["ConfirmYourEmailHtml", confirmationLink]);

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            emailSender.SendEmailAsync(email, localizer["ResetYourPassword"], localizer["ResetYourPasswordCodeHtml", resetCode]);

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            emailSender.SendEmailAsync(email, localizer["ResetYourPassword"], localizer["ResetYourPasswordLinkHtml", resetLink]);
    }
}
