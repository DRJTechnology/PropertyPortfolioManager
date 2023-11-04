using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Web.Resource;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.Server.Services.Interfaces;
using System.Diagnostics;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly GraphServiceClient graphServiceClient;

        //public UserController(IUserService userService)
        public UserController(IUserService userService, GraphServiceClient graphServiceClient)
        {
            this.userService = userService;
            this.graphServiceClient = graphServiceClient;
        }

        [HttpGet]
        [Route("GetByObjectIdentifier/{userObjectIdentifier}")]
        public Task<UserDto> GetByObjectIdentifier(Guid userObjectIdentifier)
        {
            return this.userService.GetByObjectIdentifier(userObjectIdentifier);
        }

        [HttpGet]
        [Route("GetCurrent")]
        public async Task<UserDto> GetCurrent()
        {
            try
            {
                return await this.userService.GetCurrent(User);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}