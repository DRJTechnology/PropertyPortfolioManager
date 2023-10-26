namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IPortfolioDataService : IGenericDataService
    {
        Task SelectForCurrentUserAsync(int portfolioId);
    }
}
