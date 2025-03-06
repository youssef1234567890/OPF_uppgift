using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Data.Models;

public class UserProfileDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public UserSettings Settings { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}

public class UserSettings
{
    // Common settings for all roles
    [Required]
    [StringLength(50)]
    public string DisplayName { get; set; } = string.Empty;

    public bool ReceiveNotifications { get; set; } = true;
    public string Theme { get; set; } = "light";
    public string Language { get; set; } = "en";

    // Admin-specific settings
    public bool? CanManageUsers { get; set; }
    public bool? CanAssignRoles { get; set; }
    public bool? ReceiveAdminNotifications { get; set; }
    public string? DashboardLayout { get; set; }

    // User-specific settings
    public bool? ShowProfilePublicly { get; set; }
    public string? TimeZone { get; set; }
    public string[]? Preferences { get; set; }
}

public class UpdateUserProfileRequest
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public UserSettings Settings { get; set; } = new();
} 