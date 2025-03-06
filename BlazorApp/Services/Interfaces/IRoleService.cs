using BlazorApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BlazorApp.Services.Interfaces;

public interface IRoleService
{
    Task<List<RoleDto>> GetAllRolesAsync();
    Task<RoleDto?> GetRoleByIdAsync(string roleId);
    Task<IdentityResult> CreateRoleAsync(CreateRoleRequest request);
    Task<IdentityResult> UpdateRoleAsync(UpdateRoleRequest request);
    Task<IdentityResult> DeleteRoleAsync(string roleId);
    Task<List<UserRoleDto>> GetUsersInRoleAsync(string roleName);
    Task<IdentityResult> AssignRoleToUserAsync(AssignRoleRequest request);
    Task<IdentityResult> RemoveUserFromRoleAsync(AssignRoleRequest request);
    Task<bool> IsUserInRoleAsync(string userId, string roleName);
    Task<List<string>> GetUserRolesAsync(string userId);
    Task<bool> HasRequiredRoleAsync(string userId, string requiredRole);
} 