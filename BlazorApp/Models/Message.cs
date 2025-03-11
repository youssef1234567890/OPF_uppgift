using System;
using BlazorApp.Data;

public class Message
{
    public int Id { get; set; }

    // Reintroduce a computed property for backward compatibility.
    // This property returns the ApplicationUser's UserName if available.
    public string UserName => ApplicationUser?.UserName ?? string.Empty;

    public string Text { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string ChatRoom { get; set; } = string.Empty;
    public string ReplyTo { get; set; } = string.Empty;

    // Foreign key to the ApplicationUser (Identity)
    // Use a default empty string (or consider making it nullable if preferred)
    public string ApplicationUserId { get; set; } = string.Empty;

    // Navigation property; using null-forgiving operator if you're sure it will be set later.
    public ApplicationUser ApplicationUser { get; set; } = null!;
}
