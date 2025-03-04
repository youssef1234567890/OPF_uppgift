using Microsoft.AspNetCore.Authorization;

namespace BlazorApp.Components.Admin
{
    [Authorize(Roles = "MainAdmin,Admin")]
    public partial class Admin
    {
        // Component logic
    }
} 