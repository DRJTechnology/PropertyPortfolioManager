using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IPortfolioDataService : IGenericDataService<PortfolioModel>
    {
        Task SelectForCurrentUserAsync(int portfolioId);
    }
}
