using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Server.Services.Interfaces;
using System.Globalization;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionDetailController : BaseController
    {
        private readonly ILogger<TransactionDetailController> logger;
        private readonly ITransactionDetailService transactionDetailService;

        public TransactionDetailController(ILogger<TransactionDetailController> logger, IUserService userService, ITransactionDetailService transactionDetailService)
            : base(userService)
        {
            this.logger = logger;
            this.transactionDetailService = transactionDetailService;
        }

        [HttpGet]
        [Route("GetList/{fromDate}/{toDate}/{accountId}/{transactionTypeId}")]
        public async Task<IActionResult> GetList(string fromDate, string toDate, int accountId, int transactionTypeId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return this.Ok(new List<TransactionDetailResponseModel>());
                }
                else
                {
                    DateTime from = DateTime.ParseExact(fromDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                    DateTime to = DateTime.ParseExact(toDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                    var returnData = await this.transactionDetailService.GetAsync((int)portfolioId, from, to, accountId, transactionTypeId);
                    return this.Ok(returnData);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetList/{fromDate}/{toDate}/{accountId}/{transactionTypeId}");
                return this.BadRequest();
            }

        }
    }
}
