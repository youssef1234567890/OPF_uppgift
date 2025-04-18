using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using BlazorApp.Data;
using System.Threading.Tasks;

namespace BlazorApp.Components.Account
{
    // This class provides a no-operation email sender implementation for the identity system. 
    // It supports sending confirmation links, password reset links, and reset codes, 
    // primarily for testing or scenarios where email delivery is not required.
    internal sealed class IdentityNoOpEmailSender : IEmailSender, IEmailSender<ApplicationUser>
    {
        private readonly IEmailSender _innerEmailSender = new NoOpEmailSender();

        // Implementation for non-generic interface
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return _innerEmailSender.SendEmailAsync(email, subject, htmlMessage);
        }

        // Implementation for generic interface helper methods:
        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
            SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
    }
}
