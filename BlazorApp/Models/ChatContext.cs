using Microsoft.EntityFrameworkCore;

public class ChatContext : DbContext
{
    // Constructor that accepts DbContextOptions and passes them to the base DbContext class
    public ChatContext(DbContextOptions<ChatContext> options) : base(options)
    {
    }

    // DbSet representing the collection of Message entities in the database
    public DbSet<Message> Messages { get; set; }
}