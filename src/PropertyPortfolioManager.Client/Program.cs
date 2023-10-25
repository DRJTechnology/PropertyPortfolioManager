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
// TODO set back to AddScoped
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PropertyPortfolioManager.ServerAPI"));

// TODO set back to AddScoped
builder.Services.AddTransient<IPortfolioDataService, PortfolioDataService>();
builder.Services.AddTransient<IUnitDataService, UnitDataService>();
builder.Services.AddTransient<IUnitTypeDataService, UnitTypeDataService>();
builder.Services.AddTransient<ProfileState>();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("api://2956eaa1-1d62-448d-9ac3-58ea18e4f302/API.Access");
});

await builder.Build().RunAsync();
