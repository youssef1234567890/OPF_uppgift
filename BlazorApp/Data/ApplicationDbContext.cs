using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlazorApp.Data
{
    // Your ApplicationUser should be defined elsewhere in your project.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet for persisting chat messages
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }

    // ChatMessage entity used to store messages in the database.
    public class ChatMessage
    {
        public int Id { get; set; }  // Primary key

        // Set default values to avoid null warnings
        public string User { get; set; } = "";
        public string Text { get; set; } = "";
        public string Channel { get; set; } = "";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
