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

// Configure ChatContext for your chat data (Messages table)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=chat.db"));

// Set up Blazor authentication and authorization
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// Configure the application database context for Identity.
// For a unified database, point this to the same chat.db file.
// Ensure your connection string (or fallback) is set correctly.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
?? "Data Source=chat.db;Cache=Shared";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, IdentityNoOpEmailSender>();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


// Configure a no-op email sender (for development/testing purposes).
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Seed roles and main admin account before the app starts.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleInitializer.InitializeAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Expose migration endpoint in development
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Map Blazor components and interactive server components.
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map additional endpoints required for Identity (e.g., for login, registration).
app.MapAdditionalIdentityEndpoints();

app.Run();
