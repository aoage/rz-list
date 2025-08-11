using rz_list.Components;
using Microsoft.EntityFrameworkCore;
using Data;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using rz_list.Components.Account;
using Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<RzListDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthorization(options =>
{
    foreach (UserPermissions perm in Enum.GetValues(typeof(UserPermissions)))
    {
        options.AddPolicy(
            $"Require{perm}",
            policy => policy.Requirements.Add(new PermissionRequirement(perm))
        );
    }
});
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<RzListDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<User>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

// Development endpoints for database seeding
if (app.Environment.IsDevelopment())
{
    app.MapGet("/dev/seed-database", async (IDbContextFactory<Data.RzListDbContext> dbFactory) =>
    {
        await rz_list.Utilities.DatabaseUtility.SeedDatabase(dbFactory);
        var stats = await rz_list.Utilities.DatabaseUtility.GetDatabaseStats(dbFactory);
        return Results.Ok(new { message = "Database seeded successfully!", stats });
    });

    app.MapGet("/dev/clear-and-reseed", async (IDbContextFactory<Data.RzListDbContext> dbFactory) =>
    {
        await rz_list.Utilities.DatabaseUtility.ClearAndReseedDatabase(dbFactory);
        var stats = await rz_list.Utilities.DatabaseUtility.GetDatabaseStats(dbFactory);
        return Results.Ok(new { message = "Database cleared and reseeded successfully!", stats });
    });

    app.MapGet("/dev/database-stats", async (IDbContextFactory<Data.RzListDbContext> dbFactory) =>
    {
        var stats = await rz_list.Utilities.DatabaseUtility.GetDatabaseStats(dbFactory);
        return Results.Ok(stats);
    });
}

app.Run();
