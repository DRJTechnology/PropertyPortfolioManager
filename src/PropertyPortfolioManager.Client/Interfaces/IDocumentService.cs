using PropertyPortfolioManager.Models.Model.Document;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IDocumentService : IGenericDataService
    {
        Task<List<DriveItemModel>> GetCurrentFolderContentsAsync();
    }
}
