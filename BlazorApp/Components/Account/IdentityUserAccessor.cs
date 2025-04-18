using Microsoft.AspNetCore.Identity;
using BlazorApp.Data;

namespace BlazorApp.Components.Account;

// This class provides a utility to access the currently authenticated user in the identity system. 
// It ensures that a valid user is retrieved and redirects to an error page if the user cannot be found.
internal sealed class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
{
    public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
    {
        var user = await userManager.GetUserAsync(context.User);

        if (user is null)
        {
            redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
        }

        return user;
    }
}
