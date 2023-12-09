using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Services.Interfaces;
using System.Globalization;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionDetailController : BaseController
    {
        private readonly ITransactionDetailService transactionDetailService;

        public TransactionDetailController(IUserService userService, ITransactionDetailService transactionDetailService)
            : base(userService)
        {
            this.transactionDetailService = transactionDetailService;
        }

        [HttpGet]
        [Route("GetList/{fromDate}/{toDate}/{accountId}/{transactionTypeId}")]
        public async Task<List<TransactionDetailResponseModel>> GetList(string fromDate, string toDate, int accountId, int transactionTypeId)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new List<TransactionDetailResponseModel>();
            }
            else
            {
                DateTime from = DateTime.ParseExact(fromDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                DateTime to = DateTime.ParseExact(toDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                var returnData = await this.transactionDetailService.GetAsync((int)portfolioId, from, to, accountId, transactionTypeId);
                return returnData;
            }
        }
    }
}
