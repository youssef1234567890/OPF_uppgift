using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;

        var categories = new List<Category>
        {
            new Category { Name = "Film", Description = "Diskutera filmer och biosläpp", IconEmoji = "🎬" },
            new Category { Name = "Games", Description = "Allt om spel och gaming", IconEmoji = "🎮" },
            new Category { Name = "Series", Description = "TV-serier och streaming", IconEmoji = "📺" },
        };

        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();

        var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "mainadmin@example.com");
        if (adminUser == null) return;

        var threads = new List<Thread>
        {
            new Thread { Title = "Bästa filmen 2024?", Description = "Vad tycker ni var årets bästa film?", Category = "Film", CategoryId = categories[0].Id, ApplicationUserId = adminUser.Id, CreatedAt = DateTime.UtcNow },
            new Thread { Title = "Rekommenderade spel", Description = "Tips på bra spel att spela just nu", Category = "Games", CategoryId = categories[1].Id, ApplicationUserId = adminUser.Id, CreatedAt = DateTime.UtcNow },
            new Thread { Title = "Serier att binge-watcha", Description = "Dela med er av era favoritserier!", Category = "Series", CategoryId = categories[2].Id, ApplicationUserId = adminUser.Id, CreatedAt = DateTime.UtcNow },
        };

        context.Threads.AddRange(threads);
        await context.SaveChangesAsync();
    }
}