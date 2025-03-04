namespace BlazorApp.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task AssignRoleAsync(string email, string role);
    }
} 