using Microsoft.AspNetCore.Identity;

namespace BlazorApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Add any additional properties here
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public ICollection<Thread> Threads { get; set; } = new List<Thread>();
    }
}
