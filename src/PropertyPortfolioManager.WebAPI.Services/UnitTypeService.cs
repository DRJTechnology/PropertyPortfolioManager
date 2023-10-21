using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Services
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

        public async Task<int> Create(int currentUserId, int portfolioId, UnitTypeModel unit)
        {
            var unitTypeDto = this.mapper.Map<UnitTypeDto>(unit);
            return await this.unitTypeRepository.Create(currentUserId, portfolioId, unitTypeDto);
        }

        public async Task<bool> Delete(int currentUserId, int portfolioId, int unitTypeId)
        {
            return await this.unitTypeRepository.Delete(currentUserId, portfolioId, unitTypeId);
        }

        public async Task<List<UnitTypeModel>> GetAll(int portfolioId, bool activeOnly)
        {
            var unitTypeList = await this.unitTypeRepository.GetAll(portfolioId, activeOnly);
            return this.mapper.Map<List<UnitTypeModel>>(unitTypeList);
        }

        public async Task<UnitTypeModel> GetById(int unitId, int portfolioId)
        {
            var unit = await this.unitTypeRepository.GetById(unitId, portfolioId);
            return this.mapper.Map<UnitTypeModel>(unit);
        }

        public async Task<bool> Update(int currentUserId, int portfolioId, UnitTypeModel unit)
        {
            var unitTypeDto = this.mapper.Map<UnitTypeDto>(unit);
            return await this.unitTypeRepository.Update(currentUserId, portfolioId, unitTypeDto);
        }
    }
}
