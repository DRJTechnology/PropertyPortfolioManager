using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Document;
using System.Net.Http.Json;
using System.Web;

namespace PropertyPortfolioManager.Client.Services
{
    public class DocumentService : GenericDataService, IDocumentService
    {
        public DocumentService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "Document";
        }

        public async Task<DriveItemModel> GetFolderAsync(string driveId)
        {
            try
            {
                string path = $"api/Document/GetFolder/{HttpUtility.UrlEncode(driveId)}";
                return await httpClient.GetFromJsonAsync<DriveItemModel>(path);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DriveItemModel> GetFolderAsync()
        {
            return await httpClient.GetFromJsonAsync<DriveItemModel>($"api/Document/GetFolder");
        }

        public async Task<string> GetImageBase64FromDriveItemId(string driveItemid)
        {
            try
            {
                string path = $"api/Document/ImageBase64/{HttpUtility.UrlEncode(driveItemid)}";
                var imageContent = await httpClient.GetFromJsonAsync<ImageContent>(path);
                return imageContent.ImageBase64;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
