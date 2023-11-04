using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Document;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Services
{
    public class DocumentService : GenericDataService,  IDocumentService
    {
        public DocumentService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "Document";
        }

        public async Task<List<DriveItemModel>> GetCurrentFolderContentsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<DriveItemModel>>($"api/Document/GetFolderContents");
        }
    }
}
