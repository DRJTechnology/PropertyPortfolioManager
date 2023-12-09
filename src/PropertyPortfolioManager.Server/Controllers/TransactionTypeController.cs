using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : BaseController
    {
        private readonly ITransactionTypeService transactionTypeService;

        public TransactionTypeController(IUserService userService, ITransactionTypeService transactionTypeService)
            : base(userService)
        {
            this.transactionTypeService = transactionTypeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<EntityTypeBasicModel>> GetAll()
        {
            return await this.transactionTypeService.GetAll();
        }
    }
}
