using AutoMapper;
using Microsoft.Extensions.Options;
using PropertyPortfolioManager.Models.Automapper;
using PropertyPortfolioManager.Server.Shared.Configuration;

namespace PropertyPortfolioManager.Server.Services.Tests.Extensions
{
    public static class TestExtensions
    {
        public static IMapper MapperInstance()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }

        public static IOptions<Settings> SettingsMock()
        {
            return Options.Create(
                new Settings()
                {
                    SharepointSettings = new SharepointSettings()
                    {
                        DriveId = string.Empty,
                        SiteId = string.Empty,
                    }
                }
                );
        }
    }
}
