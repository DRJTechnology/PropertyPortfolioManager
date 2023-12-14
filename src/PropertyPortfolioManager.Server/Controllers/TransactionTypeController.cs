using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : BaseController
    {
        private readonly ILogger<TransactionTypeController> logger;
        private readonly ITransactionTypeService transactionTypeService;

        public TransactionTypeController(ILogger<TransactionTypeController> logger, IUserService userService, ITransactionTypeService transactionTypeService)
            : base(userService)
        {
            this.logger = logger;
            this.transactionTypeService = transactionTypeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var returnVal = await this.transactionTypeService.GetAll();
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
