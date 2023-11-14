using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Services
{
    public class TenancyTypeService : ITenancyTypeService
    {
        private readonly ITenancyTypeRepository tenancyTypeRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public TenancyTypeService(ITenancyTypeRepository tenancyTypeRepository, ICacheService cacheService, IMapper mapper)
        {
            this.tenancyTypeRepository = tenancyTypeRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<int> Create(int currentUserId, int portfolioId, EntityTypeModel tenancyType)
        {
            var tenancyTypeDto = this.mapper.Map<EntityTypeDto>(tenancyType);
            return await this.tenancyTypeRepository.Create(currentUserId, portfolioId, tenancyTypeDto);
        }

        public async Task<bool> Delete(int currentUserId, int portfolioId, int tenancyTypeId)
        {
            return await this.tenancyTypeRepository.Delete(currentUserId, portfolioId, tenancyTypeId);
        }

        public async Task<List<EntityTypeModel>> GetAll(int portfolioId, bool activeOnly)
        {
            var tenancyTypeList = await this.tenancyTypeRepository.GetAll(portfolioId, activeOnly);
            return this.mapper.Map<List<EntityTypeModel>>(tenancyTypeList);
        }

        public async Task<EntityTypeModel> GetById(int tenancyTypeId, int portfolioId)
        {
            var tenancyType = await this.tenancyTypeRepository.GetById(tenancyTypeId, portfolioId);
            return this.mapper.Map<EntityTypeModel>(tenancyType);
        }

        public async Task<bool> Update(int currentUserId, int portfolioId, EntityTypeModel tenancyType)
        {
            var tenancyTypeDto = this.mapper.Map<EntityTypeDto>(tenancyType);
            return await this.tenancyTypeRepository.Update(currentUserId, portfolioId, tenancyTypeDto);
        }
    }
}
