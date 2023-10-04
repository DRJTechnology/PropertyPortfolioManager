using AutoMapper;
using DRJTechnology.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Web;
using PropertyPortfolioManager.Models.Automapper;
using PropertyPortfolioManager.WebAPI.Repositories;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi()
            .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
            .AddInMemoryTokenCaches();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IUnitTypeService, UnitTypeService>();
builder.Services.AddScoped<IContactTypeService, ContactTypeService>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IUnitTypeRepository, UnitTypeRepository>();
builder.Services.AddScoped<IContactTypeRepository, ContactTypeRepository>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

builder.Services.AddSingleton<IDbConnection>(db => new SqlConnection(builder.Configuration.GetConnectionString("PpmDatabaseConnectionString")));

// Set up caching.
var keyPrefix = builder.Configuration.GetValue<string>("DRJCache:KeyPrefix");
builder.Services.AddDistributedCache(opt =>
{
    opt.Enabled = builder.Configuration.GetValue<bool>("DRJCache:Enabled");
    opt.ConnectionString = builder.Configuration.GetValue<string>("DRJCache:ConnectionString") ?? string.Empty;
    opt.KeyPrefix = $"{keyPrefix}_API_";
    opt.DefaultExpiryInMinutes = builder.Configuration.GetValue<int>("DRJCache:DefaultExpiryInMinutes");
});

// Auto Mapper Configurations
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
