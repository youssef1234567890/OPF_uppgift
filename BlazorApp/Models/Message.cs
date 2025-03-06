public class Message
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Tracks which chat room the message belongs to
    public string ChatRoom { get; set; } = string.Empty; 
}
