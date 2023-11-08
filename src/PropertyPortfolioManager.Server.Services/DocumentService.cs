﻿using AutoMapper;
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

        public async Task<DriveItemModel> GetFolderAsync(string driveItemId = "root")
        {
            try
            {
                var folderDetails = await graphServiceClient.Drives[this.settings.SharepointSettings.DriveId].Items[driveItemId].GetAsync();

                var driveItem = this.mapper.Map<DriveItemModel>(folderDetails);

                var folderItems = await graphServiceClient.Drives[this.settings.SharepointSettings.DriveId].Items[driveItemId].Children.GetAsync();
                driveItem.DriveItemList = this.mapper.Map<List<DriveItemModel>>(folderItems.Value.ToList());

                return driveItem;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
