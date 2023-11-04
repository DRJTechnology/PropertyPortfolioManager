using AutoMapper;
using DRJTechnology.Cache;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Document;
using PropertyPortfolioManager.Server.Services.Interfaces;


namespace PropertyPortfolioManager.Server.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ICacheService cacheService;
        private readonly GraphServiceClient graphServiceClient;
        private readonly IMapper mapper;

        public DocumentService(ICacheService cacheService, GraphServiceClient graphServiceClient, IMapper mapper)
        {
            this.cacheService = cacheService;
            this.graphServiceClient = graphServiceClient;
            this.mapper = mapper;
        }

        public async Task<List<DriveItemModel>> GetFolderItemsAsync(string driveItemId = "root")
        {
            try
            {
                //var user = await this.graphServiceClient.Me.GetAsync();
                //string siteId = "Add to appsettings";
                //string driveId = "Add to appsettings";

                var folderItems = await graphServiceClient.Drives[driveId].Items[driveItemId].Children.GetAsync();

                var driveItemList = this.mapper.Map<List<DriveItemModel>>(folderItems.Value.ToList());

                return driveItemList;
            }
            catch (Exception ex)
            {
                throw;
            }

            //try
            //{
            //    var cacheKey = $"{CacheKeys.KeyUserPrefix}{user.GetObjectId()}";
            //    var currentUser = await this.cacheService.GetAsync<UserDto>(cacheKey);

            //    if (currentUser != null)
            //    {
            //        return currentUser;
            //    }

            //    var userObjectIdentifier = new Guid(user.GetObjectId()!);
            //    var userDto = await this.userRepository.GetByObjectIdentifier(userObjectIdentifier);

            //    if (userDto == null)
            //    {
            //        userDto = new UserDto()
            //        {
            //            ObjectIdentifier = userObjectIdentifier,
            //            Name = user.FindFirstValue("name") ?? "No name",
            //        };
            //        userDto.Id = await this.userRepository.Create(userDto);
            //    }

            //    await this.cacheService.SetAsync(cacheKey, userDto);

            //    return userDto;
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }
    }
}
