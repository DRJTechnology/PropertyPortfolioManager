using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository unitRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public UnitService(IUnitRepository unitRepository, ICacheService cacheService, IMapper mapper)
        {
            this.unitRepository = unitRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<int> Create(int currentUserId, int portfolioId, UnitEditModel unit)
        {
            var unitDto = this.mapper.Map<UnitDto>(unit);
            return await this.unitRepository.Create(currentUserId, portfolioId, unitDto);
        }

        public async Task<bool> Delete(int currentUserId, int portfolioId, int unitId)
        {
            return await this.unitRepository.Delete(currentUserId, portfolioId, unitId);
        }

        public async Task<List<UnitBasicResponseModel>> GetAll(int portfolioId, bool activeOnly)
        {
            var unitList = await this.unitRepository.GetAll(portfolioId, activeOnly);
            return this.mapper.Map<List<UnitBasicResponseModel>>(unitList);
        }

        public async Task<UnitResponseModel> GetById(int unitId, int portfolioId)
        {
            try
            {
                var unit = await this.unitRepository.GetById(unitId, portfolioId);
                return this.mapper.Map<UnitResponseModel>(unit);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(int currentUserId, int portfolioId, UnitEditModel unit)
        {
            var unitDto = this.mapper.Map<UnitDto>(unit);
            return await this.unitRepository.Update(currentUserId, portfolioId, unitDto);
        }
    }
}
