using AutoMapper;
using DRJTechnology.Cache;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using PropertyPortfolioManager.Models.CacheKeys;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Interfaces;
using PropertyPortfolioManager.Server.Shared.Configuration;

namespace PropertyPortfolioManager.Server.Services
{
    public class UnitService : IUnitService
    {
        private Settings settings;
        private readonly IUnitRepository unitRepository;
        private readonly ICacheService cacheService;
        private readonly IDocumentService documentService;
        private readonly IMapper mapper;
        private readonly GraphServiceClient graphServiceClient;

        public UnitService(IOptions<Settings> settings, IUnitRepository unitRepository, ICacheService cacheService, IDocumentService documentService, IMapper mapper, GraphServiceClient graphServiceClient)
        {
            this.settings = settings.Value;
            this.unitRepository = unitRepository;
            this.cacheService = cacheService;
            this.documentService = documentService;
            this.mapper = mapper;
            this.graphServiceClient = graphServiceClient;
        }

        public async Task<int> Create(int currentUserId, int portfolioId, UnitEditModel unit)
        {
            var cacheKey = $"{CacheKeys.KeyUnitPrefix}{portfolioId}_{unit.Active}";
            await this.cacheService.RemoveAsync(cacheKey);

            var unitDto = this.mapper.Map<UnitDto>(unit);
            return await this.unitRepository.Create(currentUserId, portfolioId, unitDto);
        }

        public async Task<bool> Delete(int currentUserId, int portfolioId, int unitId)
        {
            var cacheKey = $"{CacheKeys.KeyUnitPrefix}{portfolioId}_True";
            await this.cacheService.RemoveAsync(cacheKey);
            cacheKey = $"{CacheKeys.KeyUnitPrefix}{portfolioId}_False";
            await this.cacheService.RemoveAsync(cacheKey);

            return await this.unitRepository.Delete(currentUserId, portfolioId, unitId);
        }

        public async Task<List<UnitBasicResponseModel>> GetAll(int portfolioId, bool activeOnly)
        {
            var cacheKey = $"{CacheKeys.KeyUnitPrefix}{portfolioId}_{activeOnly}";
            var returnList = await this.cacheService.GetAsync<List<UnitBasicResponseModel>>(cacheKey);

            if (returnList != null)
            {
                return returnList;
            }

            var unitList = await this.unitRepository.GetAll(portfolioId, activeOnly);
            returnList = new List<UnitBasicResponseModel>();

            foreach (var unit in unitList)
            {
                var basicUnit = this.mapper.Map<UnitBasicResponseModel>(unit);
                basicUnit.MainPictureBase64 = await this.documentService.GetImageBase64Async(unit.MainPictureId);
                returnList.Add(basicUnit);
            }

            await this.cacheService.SetAsync(cacheKey, returnList);

            return returnList;
        }

        public async Task<UnitResponseModel> GetById(int unitId, int portfolioId)
        {
            var unit = await this.unitRepository.GetById(unitId, portfolioId);
            var returnUnit = this.mapper.Map<UnitResponseModel>(unit);
            if (unit?.MainPicture != null)
            {
                returnUnit.MainPictureBase64 = await this.documentService.GetImageBase64Async(unit.MainPicture.ItemId);
            }
            return returnUnit;
        }

        public async Task<bool> Update(int currentUserId, int portfolioId, UnitEditModel unit)
        {
            var cacheKey = $"{CacheKeys.KeyUnitPrefix}{portfolioId}_{unit.Active}";
            await this.cacheService.RemoveAsync(cacheKey);

            var unitDto = this.mapper.Map<UnitDto>(unit);
            return await this.unitRepository.Update(currentUserId, portfolioId, unitDto);
        }
    }
}
