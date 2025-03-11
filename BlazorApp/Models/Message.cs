using BlazorApp.Data;

public class Message
{
    public int Id { get; set; }

    // Computed property (if ApplicationUser is loaded) for displaying the username.
    public string UserName => ApplicationUser?.UserName ?? string.Empty;

    public string Text { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    // The original ChatRoom property can be used for live chat rooms.
    public string ChatRoom { get; set; } = string.Empty;
    
    public string ReplyTo { get; set; } = string.Empty;

    // New: Thread foreign key (optional, if null then it's a general chat message)
    public int? ThreadId { get; set; }
    public Thread? Thread { get; set; }

    // Relationship with ApplicationUser (already exists in your updated model)
    public string ApplicationUserId { get; set; } = string.Empty;
    public ApplicationUser ApplicationUser { get; set; } = null!;
}
