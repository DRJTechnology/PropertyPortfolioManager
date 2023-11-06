using PropertyPortfolioManager.Server;

var builder = WebApplication.CreateBuilder(args);

//ConfigureServices(builder);
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

app.Run();

//static void ConfigureServices(WebApplicationBuilder builder)
//{

//    // Add services to the container.
//    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
//            .EnableTokenAcquisitionToCallDownstreamApi()
//                .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
//                .AddInMemoryTokenCaches();

//    builder.Services.AddControllersWithViews();
//    builder.Services.AddRazorPages();

//    builder.Services.AddScoped<IUserService, UserService>();
//    builder.Services.AddScoped<IPortfolioService, PortfolioService>();
//    builder.Services.AddScoped<IContactService, ContactService>();
//    builder.Services.AddScoped<IContactTypeService, ContactTypeService>();
//    builder.Services.AddScoped<IUnitTypeService, UnitTypeService>();
//    builder.Services.AddScoped<IUnitService, UnitService>();
//    builder.Services.AddScoped<IDocumentService, DocumentService>();
//    builder.Services.AddScoped<IUserRepository, UserRepository>();
//    builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
//    builder.Services.AddScoped<IContactRepository, ContactRepository>();
//    builder.Services.AddScoped<IContactTypeRepository, ContactTypeRepository>();
//    builder.Services.AddScoped<IUnitTypeRepository, UnitTypeRepository>();
//    builder.Services.AddScoped<IUnitRepository, UnitRepository>();

//    //builder.Services.AddSingleton<IDbConnection>(db => new SqlConnection(builder.Configuration.GetConnectionString("PpmDatabaseConnectionString")));
//    builder.Services.AddTransient<IDbConnection>(db => new SqlConnection(builder.Configuration.GetConnectionString("PpmDatabaseConnectionString")));



//    // Set up caching.
//    var keyPrefix = builder.Configuration.GetValue<string>("DRJCache:KeyPrefix");
//    builder.Services.AddDistributedCache(opt =>
//    {
//        opt.Enabled = builder.Configuration.GetValue<bool>("DRJCache:Enabled");
//        opt.ConnectionString = builder.Configuration.GetValue<string>("DRJCache:ConnectionString") ?? string.Empty;
//        opt.KeyPrefix = $"{keyPrefix}_API_";
//        opt.DefaultExpiryInMinutes = builder.Configuration.GetValue<int>("DRJCache:DefaultExpiryInMinutes");
//    });

//    // Auto Mapper Configurations
//    var mappingConfig = new MapperConfiguration(mc =>
//    {
//        mc.AddProfile(new MappingProfile());
//    });
//    IMapper mapper = mappingConfig.CreateMapper();
//    builder.Services.AddSingleton(mapper);
//}