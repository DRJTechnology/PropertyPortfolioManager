using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebUI.Interfaces
{
    public interface IPortfolioService
    {
        Task<List<PortfolioModel>> GetAll(bool activeOnly);
        Task<PortfolioModel> GetById(int portfolioId);
        Task<int> Create(PortfolioModel portfolio);
        Task<bool> Update(PortfolioModel portfolio);
        Task<PortfolioModel> GetCurrent();
        Task<bool> SelectForCurrentUser(int id);
    }
}
