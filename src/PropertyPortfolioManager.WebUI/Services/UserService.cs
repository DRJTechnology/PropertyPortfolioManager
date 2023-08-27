using Microsoft.Graph;
using Microsoft.Identity.Abstractions;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.WebUI.Controllers;
using PropertyPortfolioManager.WebUI.Interfaces;
using System.Security.Cryptography.Pkcs;

namespace PropertyPortfolioManager.WebUI.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<HomeController> _logger;
        private IDownstreamApi _downstreamApi;

        public UserService(ILogger<HomeController> logger, IDownstreamApi ppmService)
        {
            _logger = logger;
            _downstreamApi = ppmService;
        }

        public async Task<UserDto> GetByObjectIdentifier(Guid objectIdentifier)
        {
            try
            {
                //var user = await _graphServiceClient.Me.Request().GetAsync();

                string relativePath = $"/api/User/GetByObjectIdentifier/{objectIdentifier}";

                var value = await _downstreamApi.GetForUserAsync<UserDto>(
                         "PpmWebApi",
                         options => options.RelativePath = relativePath);
                return value;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserDto> GetCurrent()
        {
            try
            {
                string relativePath = $"/api/User/GetCurrent";

                var value = await _downstreamApi.GetForUserAsync<UserDto>(
                         "PpmWebApi",
                         options => options.RelativePath = relativePath);
                return value;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
