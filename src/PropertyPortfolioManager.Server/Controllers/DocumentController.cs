using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Microsoft.Identity.Web.Resource;
using PropertyPortfolioManager.Models.Model.Document;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> logger;
        private readonly IDocumentService documentService;

        public DocumentController(ILogger<DocumentController> logger, IDocumentService documentService)
        {
            this.logger = logger;
            this.documentService = documentService;
        }

        [HttpGet]
        [Route("GetFolder")]
        public async Task<IActionResult> GetFolder()
        {
            try
            {
                var returnVal = await this.documentService.GetFolderAsync();
                return this.Ok(returnVal);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetAll");
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetFolder/{driveId}")]
        public async Task<IActionResult> GetFolder(string driveId)
        {
            try
            {
                var returnVal = await this.documentService.GetFolderAsync(driveId);
                return this.Ok(returnVal);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetFolder/{driveId}");
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("ImageBase64/{driveItemId}")]
        public async Task<IActionResult> ImageBase64(string driveItemId)
        {
            try
            {
                var imageContent = new ImageContent() { DriveItemId = driveItemId };
                imageContent.ImageBase64 = await this.documentService.GetImageBase64Async(driveItemId);
                return this.Ok(imageContent);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"ImageBase64/{driveItemId}");
                return this.BadRequest();
            }
        }
    }
}