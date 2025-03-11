using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Data; // Ensure this namespace is included so ApplicationUser is found

public class ChatContext : IdentityDbContext<ApplicationUser>
{
    public ChatContext(DbContextOptions<ChatContext> options) : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Configure the relationship between Message and ApplicationUser
        builder.Entity<Message>()
            .HasOne(m => m.ApplicationUser)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.ApplicationUserId)
            .IsRequired();
    }
}
