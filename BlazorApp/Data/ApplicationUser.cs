using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlazorApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Optional: add extra properties for your application
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
