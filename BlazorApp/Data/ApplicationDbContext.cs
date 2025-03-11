using BlazorApp.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Configure the relationship between Message and ApplicationUser
        builder.Entity<Message>()
            .HasOne(m => m.ApplicationUser)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade); // This will cascade delete messages when the user is deleted.
    }

    public DbSet<Message> Messages { get; set; }  // Add this line
}
