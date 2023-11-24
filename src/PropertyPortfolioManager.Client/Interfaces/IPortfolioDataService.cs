using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IPortfolioDataService : IGenericDataService
    {
        Task SelectForCurrentUserAsync(int portfolioId);
        Task<PortfolioModel> GetCurrentAsync();
    }
}
