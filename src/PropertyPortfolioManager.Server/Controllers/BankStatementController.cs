using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class BankStatementController : BaseController
    {
        private readonly ILogger<BankStatementController> logger;
        private readonly IDocumentService documentService;
        private readonly IBankStatementService bankStatementService;

        public BankStatementController(ILogger<BankStatementController> logger, IUserService userService, IBankStatementService bankStatementService)
        : base(userService)
        {
            this.logger = logger;
            this.bankStatementService = bankStatementService;
        }

        [HttpPost]
        [Route("UploadBankStatement")]
        public async Task<IActionResult> UploadBankStatement()
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return Ok( new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "UploadBankStatemente: User has no Selected Portfolio Id set."
                    });
                }
                else
                {
                    var file = Request.Form.Files[0];
                    if (file == null || file.Length == 0)
                        return BadRequest("Please select a file to upload.");

                    var stream = file.OpenReadStream();
                    await this.bankStatementService.UploadBankStatement((await this.GetCurrentUser()).Id, (int)portfolioId, stream);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}