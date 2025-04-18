using BlazorApp.Data;

public class Message
{
    public int Id { get; set; }

    // Computed property (if ApplicationUser is loaded) for displaying the username.
    public string UserName => ApplicationUser?.UserName ?? string.Empty;

    public string Text { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    public string ChatRoom { get; set; } = string.Empty;
    
    public string ReplyTo { get; set; } = string.Empty;

    public int? ThreadId { get; set; }
    public Thread? Thread { get; set; }

    public string ApplicationUserId { get; set; } = string.Empty;
    public ApplicationUser ApplicationUser { get; set; } = null!;

    // ─── Added for cascade delete of replies ───
    public int? ParentMessageId { get; set; }
    public Message? ParentMessage { get; set; }
    public ICollection<Message> Replies { get; set; } = new List<Message>();
}