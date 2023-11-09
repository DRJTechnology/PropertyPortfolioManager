using Microsoft.Graph.Models;
using PropertyPortfolioManager.Models.Model.Document;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<DriveItemModel> GetFolderAsync(string driveItemId = "root");
        Task<string> GetImageBase64Async(string driveItemId);
    }
}
