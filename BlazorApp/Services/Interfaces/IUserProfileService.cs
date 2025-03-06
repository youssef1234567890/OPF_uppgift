using BlazorApp.Data.Models;

namespace BlazorApp.Services.Interfaces;

public interface IUserProfileService
{
    Task<UserProfileDto> GetUserProfileAsync(string userId);
    Task<bool> UpdateUserProfileAsync(UpdateUserProfileRequest request);
    Task<List<string>> GetUserPermissionsAsync(string userId);
    Task<bool> UpdateUserSettingsAsync(string userId, UserSettings settings);
} 