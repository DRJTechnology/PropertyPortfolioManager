using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PropertyPortfolioManager.Client;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Client.Services;
using PropertyPortfolioManager.Client.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("PropertyPortfolioManager.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PropertyPortfolioManager.ServerAPI"));

builder.Services.AddScoped<IPortfolioDataService, PortfolioDataService>();
builder.Services.AddScoped<IUnitDataService, UnitDataService>();
builder.Services.AddScoped<IUnitTypeDataService, UnitTypeDataService>();
builder.Services.AddScoped<IContactDataService, ContactDataService>();
builder.Services.AddScoped<IContactTypeDataService, ContactTypeDataService>();
builder.Services.AddScoped<IUserDataService, UserDataService>();
builder.Services.AddScoped<ProfileState>();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("api://2956eaa1-1d62-448d-9ac3-58ea18e4f302/API.Access");
});

await builder.Build().RunAsync();
