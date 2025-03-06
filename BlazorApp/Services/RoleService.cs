using BlazorApp.Common.Constants;
using BlazorApp.Data;
using BlazorApp.Data.Models;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public RoleService(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _context = context;
    }

    public async Task<List<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles
            .Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name!,
                CreatedAt = DateTime.UtcNow, // You might want to add these fields to your IdentityRole
                UpdatedAt = null
            })
            .ToListAsync();

        return roles;
    }

    public async Task<RoleDto?> GetRoleByIdAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null) return null;

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name!,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    public async Task<IdentityResult> CreateRoleAsync(CreateRoleRequest request)
    {
        var role = new IdentityRole(request.Name);
        return await _roleManager.CreateAsync(role);
    }

    public async Task<IdentityResult> UpdateRoleAsync(UpdateRoleRequest request)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = "The specified role was not found."
            });
        }

        role.Name = request.Name;
        return await _roleManager.UpdateAsync(role);
    }

    public async Task<IdentityResult> DeleteRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = "The specified role was not found."
            });
        }

        return await _roleManager.DeleteAsync(role);
    }

    public async Task<List<UserRoleDto>> GetUsersInRoleAsync(string roleName)
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
        var userRoleDtos = new List<UserRoleDto>();

        foreach (var user in usersInRole)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userRoleDtos.Add(new UserRoleDto
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                Roles = roles.ToList()
            });
        }

        return userRoleDtos;
    }

    public async Task<IdentityResult> AssignRoleToUserAsync(AssignRoleRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = "The specified user was not found."
            });
        }

        if (!await _roleManager.RoleExistsAsync(request.RoleName))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = "The specified role was not found."
            });
        }

        return await _userManager.AddToRoleAsync(user, request.RoleName);
    }

    public async Task<IdentityResult> RemoveUserFromRoleAsync(AssignRoleRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = "The specified user was not found."
            });
        }

        return await _userManager.RemoveFromRoleAsync(user, request.RoleName);
    }

    public async Task<bool> IsUserInRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        return await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task<List<string>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return new List<string>();

        var roles = await _userManager.GetRolesAsync(user);
        return roles.ToList();
    }

    public async Task<bool> HasRequiredRoleAsync(string userId, string requiredRole)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var userRoles = await _userManager.GetRolesAsync(user);
        
        // Check if user has the required role or a higher role in the hierarchy
        if (!RoleConstants.RoleHierarchy.TryGetValue(requiredRole, out var allowedRoles))
        {
            return false;
        }

        return userRoles.Any(role => allowedRoles.Contains(role));
    }
} 