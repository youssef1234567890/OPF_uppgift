using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ChatContextFactory : IDesignTimeDbContextFactory<ChatContext>
{
    // Creates a new instance of ChatContext with the specified options
    public ChatContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ChatContext>();
        
        // Configures the context to use a SQLite database with the specified connection string
        optionsBuilder.UseSqlite("Data Source=chat.db");

        // Returns a new instance of ChatContext with the configured options
        return new ChatContext(optionsBuilder.Options);
    }
}
