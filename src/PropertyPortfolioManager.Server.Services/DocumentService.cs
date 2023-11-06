using AutoMapper;
using DRJTechnology.Cache;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using PropertyPortfolioManager.Models.Model.Document;
using PropertyPortfolioManager.Server.Services.Interfaces;
using PropertyPortfolioManager.Server.Shared.Configuration;

namespace PropertyPortfolioManager.Server.Services
{
    public class DocumentService : IDocumentService
    {
        private Settings settings;
        private readonly ICacheService cacheService;
        private readonly GraphServiceClient graphServiceClient;
        private readonly IMapper mapper;

        public DocumentService(IOptions<Settings> settings, ICacheService cacheService, GraphServiceClient graphServiceClient, IMapper mapper)
        {
            this.settings = settings.Value;
            this.cacheService = cacheService;
            this.graphServiceClient = graphServiceClient;
            this.mapper = mapper;
        }

        public async Task<List<DriveItemModel>> GetFolderItemsAsync(string driveItemId = "root")
        {
            try
            {
                var folderItems = await graphServiceClient.Drives[this.settings.SharepointSettings.DriveId].Items[driveItemId].Children.GetAsync();

                var driveItemList = this.mapper.Map<List<DriveItemModel>>(folderItems.Value.ToList());

                return driveItemList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
