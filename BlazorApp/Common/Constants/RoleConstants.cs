namespace BlazorApp.Common.Constants;

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

    // Helper method to check if a role has higher or equal privilege
    public static bool HasPrivilege(string currentRole, string requiredRole)
    {
        if (!RoleHierarchy.ContainsKey(currentRole))
            return false;

        return RoleHierarchy[currentRole].Contains(requiredRole);
    }
} 