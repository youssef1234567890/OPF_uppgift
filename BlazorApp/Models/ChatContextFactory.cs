using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ChatContextFactory : IDesignTimeDbContextFactory<ChatContext>
{
    public ChatContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ChatContext>();
        optionsBuilder.UseSqlite("Data Source=chat.db");

        return new ChatContext(optionsBuilder.Options);
    }
}
