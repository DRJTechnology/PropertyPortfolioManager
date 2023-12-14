using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web.Resource;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;
        private readonly GraphServiceClient graphServiceClient;

        public UserController(ILogger<UserController> logger, IUserService userService, GraphServiceClient graphServiceClient)
        {
            this.logger = logger;
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
        public async Task<IActionResult> GetCurrent()
        {
            try
            {
                var returnVal = await this.userService.GetCurrent(User);
                return Ok(returnVal);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetCurrent");
                return this.BadRequest();
            }
        }
    }
}