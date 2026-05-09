using System.ComponentModel.DataAnnotations;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string IconEmoji { get; set; } = "📑";

    public ICollection<Thread> Threads { get; set; } = new List<Thread>();
}