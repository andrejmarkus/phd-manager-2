﻿using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using PhDManager;
using PhDManager.Components;
using PhDManager.Components.Account;
using PhDManager.Data;
using PhDManager.Models.Options;
using PhDManager.Models.Roles;
using PhDManager.Services;
using PhDManager.Services.IRepositories;
using PhDManager.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddLocalization();
builder.Services.AddControllers();

builder.Services.Configure<ActiveDirectoryOptions>(builder.Configuration.GetSection(ActiveDirectoryOptions.ActiveDirectory));
builder.Services.Configure<GoogleOptions>(builder.Configuration.GetSection(GoogleOptions.Google));
builder.Services.Configure<EmailSenderOptions>(builder.Configuration.GetSection(EmailSenderOptions.EmailSender));

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        var googleOptions = builder.Configuration.GetSection(GoogleOptions.Google).Get<GoogleOptions>() ?? throw new InvalidOperationException("Google options not found.");
        options.ClientId = googleOptions.ClientId;
        options.ClientSecret = googleOptions.ClientSecret;
    });

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

var databaseConnectionString = builder.Configuration.GetSection(DatabaseOptions.Database).Get<DatabaseOptions>()?.ConnectionString ?? throw new InvalidOperationException("Connection string not found.");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseNpgsql(databaseConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.WebHost.UseSentry(options =>
{
    var sentryDsn = builder.Configuration.GetSection(SentryAppOptions.SentryApp).Get<SentryAppOptions>()?.Dsn ?? throw new InvalidOperationException("Sentry Dsn not found.");
    options.Dsn = sentryDsn;
    options.TracesSampleRate = 1.0;
});

builder.Services.AddHangfire(options => options
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(databaseConnectionString))
);
builder.Services.AddHangfireServer();

builder.Services.AddAntiforgery();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityEmailSender>();
builder.Services.AddSingleton<InformationEmailSender>();
builder.Services.AddSingleton<SchoolYearService>();
builder.Services.AddSingleton<EnumService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ActiveDirectoryService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<DocumentService>();
builder.Services.AddScoped<JobService>();
builder.Services.AddScoped<RoleInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

string[] supportedCultures = ["sk-SK", "en-US"];
app.UseRequestLocalization(new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures));

app.MapStaticAssets();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.UseHangfireDashboard("/cron/dashboard", new DashboardOptions()
{
    Authorization = [new AuthorizationFilter()]
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var roleInitializer = services.GetRequiredService<RoleInitializer>();
    var jobService = services.GetRequiredService<JobService>();

    await context.Database.MigrateAsync();
    await roleInitializer.InitializeAsync();
    jobService.Initialize();
}

app.Run();

namespace PhDManager
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            var auth = httpContext.User.Identity?.IsAuthenticated ?? false;
            var role = httpContext.User.IsInRole(Admin.Role);
            return auth && role;
        }
    }
}