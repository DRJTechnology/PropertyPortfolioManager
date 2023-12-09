using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Services
{
    public class UnitTypeService : IUnitTypeService
    {
        private readonly IUnitTypeRepository unitTypeRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public UnitTypeService(IUnitTypeRepository unitTypeRepository, ICacheService cacheService, IMapper mapper)
        {
            this.unitTypeRepository = unitTypeRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<int> Create(int currentUserId, int portfolioId, EntityTypeModel unitType)
        {
            var unitTypeDto = this.mapper.Map<EntityTypeDto>(unitType);
            return await this.unitTypeRepository.Create(currentUserId, portfolioId, unitTypeDto);
        }

        public async Task<bool> Delete(int currentUserId, int portfolioId, int unitTypeId)
        {
            return await this.unitTypeRepository.Delete(currentUserId, portfolioId, unitTypeId);
        }

        public async Task<List<EntityTypeModel>> GetAll(int portfolioId, bool activeOnly)
        {
            var unitTypeList = await this.unitTypeRepository.GetAll(portfolioId, activeOnly);
            return this.mapper.Map<List<EntityTypeModel>>(unitTypeList);
        }

        public async Task<EntityTypeModel> GetById(int unitId, int portfolioId)
        {
            var unitType = await this.unitTypeRepository.GetById(unitId, portfolioId);
            return this.mapper.Map<EntityTypeModel>(unitType);
        }

        public async Task<bool> Update(int currentUserId, int portfolioId, EntityTypeModel unitType)
        {
            var unitTypeDto = this.mapper.Map<EntityTypeDto>(unitType);
            return await this.unitTypeRepository.Update(currentUserId, portfolioId, unitTypeDto);
        }
    }
}
