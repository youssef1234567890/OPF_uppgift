using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class LogService
{
    private readonly ApplicationDbContext _dbContext;

    public LogService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IGrouping<string, Message>>> GetGroupedLogsAsync(bool isAdmin, string currentUserId, string selectedUser, bool sortDescending)
    {
        IQueryable<Message> query = _dbContext.Messages
            .Include(m => m.ApplicationUser)
            .Include(m => m.Thread)
            .Where(m => m.ChatRoom != null);

        if (!isAdmin)
        {
            query = query.Where(m => m.ApplicationUserId == currentUserId);
        }
        else if (selectedUser != "All")
        {
            query = query.Where(m => m.ApplicationUser.UserName == selectedUser);
        }

        query = sortDescending ? query.OrderByDescending(m => m.Timestamp) : query.OrderBy(m => m.Timestamp);
        var logs = await query.ToListAsync();

        return logs.GroupBy(m => m.ChatRoom).ToList();
    }

    public async Task<List<string>> GetDistinctUsersAsync()
    {
        return await _dbContext.Messages
            .Include(m => m.ApplicationUser)
            .Where(m => m.ChatRoom != null)
            .Select(m => m.ApplicationUser.UserName!)
            .Distinct()
            .OrderBy(u => u)
            .ToListAsync();
    }
}

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<LogService>();
    }
}