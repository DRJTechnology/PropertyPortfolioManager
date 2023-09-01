using AutoMapper;
using PropertyPortfolioManager.Models.Automapper;

namespace PropertyPortfolioManager.WebAPI.Services.Tests.Extensions
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
    }
}
