using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        public Task<int> Create(int userId, UnitDto newUnit)
        {
            throw new NotImplementedException();
        }

        public Task<List<UnitDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UnitDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int userId, UnitDto existingUnit)
        {
            throw new NotImplementedException();
        }
    }
}
