using PropertyPortfolioManager.Models.Model.Document;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IDocumentService : IGenericDataService
    {
        Task<DriveItemModel> GetFolderAsync();
        Task<DriveItemModel> GetFolderAsync(string driveId);
        Task<string> GetImageBase64FromDriveItemId(string driveItemid);
    }
}
