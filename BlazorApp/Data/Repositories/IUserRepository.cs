namespace BlazorApp.Data.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task AddToRoleAsync(ApplicationUser user, string role);
        // Other user-related data access methods
    }
} 