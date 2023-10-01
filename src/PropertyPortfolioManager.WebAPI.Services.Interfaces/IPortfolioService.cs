using PropertyPortfolioManager.Models.Model.Property;
using System.Security.Claims;

namespace PropertyPortfolioManager.WebAPI.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<List<PortfolioModel>> GetAll(int portfolioId);
        Task<PortfolioModel> GetById(int portfolioId, int userId);
        Task<int> Create(int currentUserId, PortfolioModel portfolio);
        Task<bool> Update(int currentUserId, PortfolioModel portfolio);
        Task<PortfolioModel> GetCurrent(ClaimsPrincipal user);
    }
}
