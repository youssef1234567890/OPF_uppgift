public class Message
{
    // Unique identifier for the message
    public int Id { get; set; }

    // Username of the person who sent the message
    public string UserName { get; set; } = string.Empty;

    // Content of the message
    public string Text { get; set; } = string.Empty;

    // Timestamp when the message was sent, defaulting to the current UTC time
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Name of the chat room where the message was sent
    public string ChatRoom { get; set; } = string.Empty;

    // Identifier of the message this message is replying to, if any
    public string ReplyTo { get; set; } = string.Empty;
}