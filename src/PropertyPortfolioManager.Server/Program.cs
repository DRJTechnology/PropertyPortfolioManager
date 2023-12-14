using NLog;
using NLog.Web;
using PropertyPortfolioManager.Server;
using PropertyPortfolioManager.Server.Middleware;

var logger = LogManager
    .Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllersWithViews();

    // Cleaer the build in provider
    builder.Logging.ClearProviders();

    // log youe application at trace level 
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

    // Register the NLog service
    builder.Host.UseNLog();

    builder.Services.ConfigureServices(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");

    app.UseMiddleware<ExceptionMiddleware>();

    app.Run();
}

catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    // Ensure to shout downon the NLog ( Disposing )
    NLog.LogManager.Shutdown();
}

