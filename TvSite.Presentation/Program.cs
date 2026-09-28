using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using TvSite.Application.Service;
using TvSite.Application.ServiceAPI;
using TvSite.Domain.Entities;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Domain.InterfacesAPI.Repositories;
using TvSite.Domain.InterfacesAPI.Services;
using TvSite.Infrastructure.Data;
using TvSite.Infrastructure.Repositories;
using TvSite.Infrastructure.RepositoryAPI;
using TvSite.Presentation.Components;
using TvSite.Presentation.Components.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Get client with IHttpClientFactory.CreateClient();
builder.Services.AddHttpClient("TmdbApiClient", client =>
{
    //Get token from user secrets
    var accessToken = builder.Configuration["Tmdb:AccessToken"];

    client.DefaultRequestHeaders.Add("accept", "application/json");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
});

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'Default' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<IAPIRepository, APIRepository>();
builder.Services.AddTransient<IAPIService, APIService>();

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

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();