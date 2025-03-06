using BlazorApp.Data;
using BlazorApp.Data.Models;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Services;

public class UserProfileService : IUserProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserProfileService> _logger;

    public UserProfileService(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
        ILogger<UserProfileService> logger)
    {
        _userManager = userManager;
        _context = context;
        _logger = logger;
    }

    public async Task<UserProfileDto> GetUserProfileAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new ArgumentException("User not found", nameof(userId));
        }

        var roles = await _userManager.GetRolesAsync(user);
        var highestRole = GetHighestRole(roles);
        var permissions = await GetUserPermissionsAsync(userId);

        // In a real application, you would load these settings from a database
        // For now, we'll create default settings based on the role
        var settings = new UserSettings
        {
            DisplayName = user.UserName ?? string.Empty,
            ReceiveNotifications = true,
            Theme = "light",
            Language = "en"
        };

        // Add role-specific settings
        if (highestRole == "MainAdmin" || highestRole == "Admin")
        {
            settings.CanManageUsers = true;
            settings.CanAssignRoles = highestRole == "MainAdmin";
            settings.ReceiveAdminNotifications = true;
            settings.DashboardLayout = "advanced";
        }
        else
        {
            settings.ShowProfilePublicly = true;
            settings.TimeZone = "UTC";
            settings.Preferences = new[] { "default" };
        }

        return new UserProfileDto
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            Role = highestRole,
            Settings = settings,
            Permissions = permissions
        };
    }

    public async Task<bool> UpdateUserProfileAsync(UpdateUserProfileRequest request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return false;
            }

            // In a real application, you would save these settings to a database
            // For now, we'll just log the update
            _logger.LogInformation("Updated profile settings for user {UserId}", request.UserId);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating profile for user {UserId}", request.UserId);
            return false;
        }
    }

    public async Task<List<string>> GetUserPermissionsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new List<string>();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var permissions = new List<string>();

        // Define role-based permissions
        if (roles.Contains("MainAdmin"))
        {
            permissions.AddRange(new[]
            {
                "manage_users",
                "assign_roles",
                "view_admin_dashboard",
                "manage_settings",
                "view_audit_logs"
            });
        }
        else if (roles.Contains("Admin"))
        {
            permissions.AddRange(new[]
            {
                "manage_users",
                "view_admin_dashboard",
                "manage_settings"
            });
        }
        else if (roles.Contains("User"))
        {
            permissions.AddRange(new[]
            {
                "edit_profile",
                "view_dashboard",
                "manage_preferences"
            });
        }

        return permissions;
    }

    public async Task<bool> UpdateUserSettingsAsync(string userId, UserSettings settings)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            // Validate settings based on user role
            var roles = await _userManager.GetRolesAsync(user);
            var highestRole = GetHighestRole(roles);

            if (highestRole == "User")
            {
                // Clear admin-specific settings for regular users
                settings.CanManageUsers = null;
                settings.CanAssignRoles = null;
                settings.ReceiveAdminNotifications = null;
                settings.DashboardLayout = null;
            }

            // In a real application, you would save these settings to a database
            // For now, we'll just log the update
            _logger.LogInformation("Updated settings for user {UserId}", userId);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating settings for user {UserId}", userId);
            return false;
        }
    }

    private string GetHighestRole(IList<string> roles)
    {
        if (roles.Contains("MainAdmin")) return "MainAdmin";
        if (roles.Contains("Admin")) return "Admin";
        return "User";
    }
} 