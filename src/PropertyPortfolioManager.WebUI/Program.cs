using DRJTechnology.Cache;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using PropertyPortfolioManager.WebUI.Helpers;
using PropertyPortfolioManager.WebUI.Interfaces;
using PropertyPortfolioManager.WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

var initialScopes = builder.Configuration.GetSection("DownstreamApi:Scopes").Get<string[]>();

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
            .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
            .AddDownstreamApi("PpmWebApi", builder.Configuration.GetSection("DownstreamApi"))
            .AddInMemoryTokenCaches();

builder.Services.AddAuthorization(options =>

{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

// TODO load mappings in separate class
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IPpmApiFacade, PpmApiFacade>();

// Set up caching.
var keyPrefix = builder.Configuration.GetValue<string>("DRJCache:KeyPrefix");
builder.Services.AddDistributedCache(opt =>
{
    opt.Enabled = builder.Configuration.GetValue<bool>("DRJCache:Enabled");
    opt.ConnectionString = builder.Configuration.GetValue<string>("DRJCache:ConnectionString") ?? string.Empty;
    opt.KeyPrefix = $"{keyPrefix}_WebUI_";
    opt.DefaultExpiryInMinutes = builder.Configuration.GetValue<int>("DRJCache:DefaultExpiryInMinutes");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllers();

app.Run();
