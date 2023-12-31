﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Repositories;
using PropertyPortfolioManager.Server.Services.Interfaces;
using PropertyPortfolioManager.Server.Services;
using System.Data;
using Microsoft.Identity.Web;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Automapper;
using PropertyPortfolioManager.Server.Shared.Configuration;
using Microsoft.Graph.Models.ExternalConnectors;

namespace PropertyPortfolioManager.Server
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            // Add services to the container.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"))
                    .EnableTokenAcquisitionToCallDownstreamApi()
                    .AddMicrosoftGraph(configuration.GetSection("MicrosoftGraph"))
                    .AddDistributedTokenCaches();

            services.AddStackExchangeRedisCache(options =>
            {
                var connectionUrl = configuration.GetConnectionString("Redis");
                options.Configuration = connectionUrl;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactTypeService, ContactTypeService>();
            services.AddScoped<IUnitTypeService, UnitTypeService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<ITenancyTypeService, TenancyTypeService>();
            services.AddScoped<ITenancyService, TenancyService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountTypeService, AccountTypeService>();
            services.AddScoped<ITransactionDetailService, TransactionDetailService>();
            services.AddScoped<ITransactionTypeService, TransactionTypeService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IBankStatementService, BankStatementService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactTypeRepository, ContactTypeRepository>();
            services.AddScoped<IUnitTypeRepository, UnitTypeRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<ITenancyTypeRepository, TenancyTypeRepository>();
            services.AddScoped<ITenancyRepository, TenancyRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddScoped<ITransactionDetailRepository, TransactionDetailRepository>();
            services.AddScoped<IBankStatementRepository, BankStatementRepository>();

            services.AddTransient<IDbConnection>(db => new SqlConnection(configuration.GetConnectionString("PpmDatabaseConnectionString")));

            services.Configure<Settings>(configuration.GetSection("Settings"));

            // Set up caching.
            var keyPrefix = configuration.GetValue<string>("DRJCache:KeyPrefix");
            services.AddDistributedCache(opt =>
            {
                opt.Enabled = configuration.GetValue<bool>("DRJCache:Enabled");
                opt.ConnectionString = configuration.GetValue<string>("DRJCache:ConnectionString") ?? string.Empty;
                opt.KeyPrefix = $"{keyPrefix}_API_";
                opt.DefaultExpiryInMinutes = configuration.GetValue<int>("DRJCache:DefaultExpiryInMinutes");
            });

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }
}
