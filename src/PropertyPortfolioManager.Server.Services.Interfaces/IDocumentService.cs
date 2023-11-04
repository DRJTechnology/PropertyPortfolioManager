using Microsoft.Graph.Models;
using PropertyPortfolioManager.Models.Model.Document;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<List<DriveItemModel>> GetFolderItemsAsync(string driveItemId = "root");
    }
}
