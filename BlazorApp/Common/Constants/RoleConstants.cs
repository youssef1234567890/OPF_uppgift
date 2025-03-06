namespace BlazorApp.Common.Constants;

/// <summary>
/// Contains all role-related constants and helper methods for role management
/// </summary>
public static class RoleConstants
{
    public const string MainAdmin = "MainAdmin";
    public const string Admin = "Admin";
    public const string User = "User";

    public static readonly string[] AllRoles = { MainAdmin, Admin, User };
    
    public static readonly Dictionary<string, string[]> RoleHierarchy = new()
    {
        { MainAdmin, AllRoles },                    // MainAdmin can manage all roles
        { Admin, new[] { Admin, User } },          // Admin can manage Admin and User roles
        { User, new[] { User } }                   // User can only access User role permissions
    };

    /// <summary>
    /// Checks if a role has higher or equal privilege compared to the required role
    /// </summary>
    /// <param name="currentRole">The role to check privileges for</param>
    /// <param name="requiredRole">The role level required</param>
    /// <returns>True if the current role has sufficient privileges</returns>
    public static bool HasPrivilege(string currentRole, string requiredRole)
    {
        if (string.IsNullOrEmpty(currentRole) || string.IsNullOrEmpty(requiredRole))
        {
            return false;
        }

        return RoleHierarchy.TryGetValue(currentRole, out var allowedRoles) && 
               allowedRoles.Contains(requiredRole);
    }
} 