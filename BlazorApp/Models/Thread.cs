using System.ComponentModel.DataAnnotations;
using BlazorApp.Data;

public class Thread
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    [Required]
    public string Category { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign key to the user who created the thread.
    public string ApplicationUserId { get; set; } = string.Empty;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    
    // Add the collection for related messages.
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
