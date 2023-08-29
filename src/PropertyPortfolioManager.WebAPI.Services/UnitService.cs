using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository unitRepository;
        private readonly ICacheService cacheService;

        public UnitService(IUnitRepository unitRepository, ICacheService cacheService)
        {
            this.unitRepository = unitRepository;
            this.cacheService = cacheService;
        }

        public Task<int> Create(int currentUserId, UnitRequestModel unit)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int currentUserId, int unitId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UnitResponseModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UnitResponseModel> GetById(int unitId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(int currentUserId, UnitRequestModel unit)
        {
            throw new NotImplementedException();
        }
    }
}
