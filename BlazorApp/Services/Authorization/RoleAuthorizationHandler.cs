using BlazorApp.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace BlazorApp.Services.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
    public string RequiredRole { get; }

    public RoleRequirement(string requiredRole)
    {
        RequiredRole = requiredRole;
    }
}

public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleRequirement requirement)
    {
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            return Task.CompletedTask;
        }

        var userRoles = context.User.Claims
            .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
            .Select(c => c.Value)
            .ToList();

        // Check if user has the required role or a higher role in the hierarchy
        if (RoleConstants.RoleHierarchy.TryGetValue(requirement.RequiredRole, out var allowedRoles))
        {
            if (userRoles.Any(role => allowedRoles.Contains(role)))
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
} 