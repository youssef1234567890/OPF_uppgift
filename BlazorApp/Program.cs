using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Components;
using BlazorApp.Components.Account;
using BlazorApp.Data;
using YourAppNamespace.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure the ChatContext (e.g., if using SQLite)
builder.Services.AddDbContext<ChatContext>(options =>
    options.UseSqlite("Data Source=chat.db")); // SQLite connection for the chat data

// Set up authentication and authorization
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// Configure the application database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Choose which DbContext to use, here it's SQLite. To switch to SQL Server, change the `UseSqlite` to `UseSqlServer`.

// Use SQLite for the ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// You can switch to SQL Server by uncommenting below and commenting the SQLite code:
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(connectionString));

// Add Identity services (this will configure authentication and role-based authorization)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure IdentityCore with roles support. For testing, you can set RequireConfirmedAccount to false.
// If you keep it true, ensure that in your RoleInitializer you mark the main admin's EmailConfirmed as true.
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to false for testing if desired.
})
    .AddRoles<IdentityRole>() // Adds role support
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// Configure email sender (no-op sender for now)
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Seed roles and the main admin account before the app starts.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleInitializer.InitializeAsync(services); // Ensure roles and admin account are created
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Migration endpoint for development
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true); // Global error handling
    app.UseHsts(); // Enforce HTTPS
}

app.UseHttpsRedirection(); // Force HTTPS for production
app.UseStaticFiles(); // Serve static files (CSS, JS, images, etc.)
app.UseAntiforgery(); // Use anti-forgery protection (important for POST requests)

// Map Blazor components and interactive server components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by Identity Razor components for login, registration, etc.
app.MapAdditionalIdentityEndpoints();

app.Run(); // Run the application
