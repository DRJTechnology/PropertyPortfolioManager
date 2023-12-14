using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : BaseController
    {
        private readonly ILogger<AccountTypeController> logger;
        private readonly IAccountTypeService accountTypeService;

        public AccountTypeController(ILogger<AccountTypeController> logger, IUserService userService, IAccountTypeService accountTypeService)
            : base(userService)
        {
            this.logger = logger;
            this.accountTypeService = accountTypeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var returnVal = await this.accountTypeService.GetAll();
                return this.Ok(returnVal);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetAll");
                return this.BadRequest();
            }
        }
    }
}
