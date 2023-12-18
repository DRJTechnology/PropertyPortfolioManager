using AutoMapper;
using DRJTechnology.Cache;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using PropertyPortfolioManager.Models.CacheKeys;
using PropertyPortfolioManager.Models.Model.Document;
using PropertyPortfolioManager.Models.Model.Property;
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

        public async Task<DriveItemModel> GetFolderAsync(string driveItemId = "root")
        {
            var folderDetails = await graphServiceClient.Drives[this.settings.SharepointSettings.DriveId].Items[driveItemId].GetAsync();

            var driveItem = this.mapper.Map<DriveItemModel>(folderDetails);

            var folderItems = await graphServiceClient.Drives[this.settings.SharepointSettings.DriveId].Items[driveItemId].Children.GetAsync();
            driveItem.DriveItemList = this.mapper.Map<List<DriveItemModel>>(folderItems.Value.ToList());

            return driveItem;
        }

        public async Task<string> GetImageBase64Async(int portfolioId, string imageId)
        {
            if (imageId.IsNullOrEmpty())
            {
                return string.Empty;
            }

            var cacheKey = $"{CacheKeys.KeyDocumentPrefix}{portfolioId}_{imageId}";
            var returnPhotoStream = await this.cacheService.GetAsync<string>(cacheKey);

            if (returnPhotoStream != null)
            {
                return returnPhotoStream;
            }

            var photoStream = await graphServiceClient.Drives[this.settings.SharepointSettings.DriveId].Items[imageId].Content.GetAsync();
            if (photoStream != null)
            {
                byte[] photoBytes;
                using (var ms = new MemoryStream())
                {
                    photoStream.CopyTo(ms);
                    photoBytes = ms.ToArray();
                }

                returnPhotoStream = Convert.ToBase64String(photoBytes);
                await this.cacheService.SetAsync(cacheKey, returnPhotoStream);

                return returnPhotoStream;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
