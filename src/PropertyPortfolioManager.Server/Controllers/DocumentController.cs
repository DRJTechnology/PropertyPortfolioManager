using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IDocumentService documentService;

        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpGet]
        [Route("GetFolder")]
        public async Task<DriveItemModel> GetFolder()
        {
            try
            {
                return await this.documentService.GetFolderAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetFolder/{driveId}")]
        public async Task<DriveItemModel> GetFolder(string driveId)
        {
            try
            {
                return await this.documentService.GetFolderAsync(driveId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}