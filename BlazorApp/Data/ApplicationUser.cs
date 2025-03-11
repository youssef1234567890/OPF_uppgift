using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlazorApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
