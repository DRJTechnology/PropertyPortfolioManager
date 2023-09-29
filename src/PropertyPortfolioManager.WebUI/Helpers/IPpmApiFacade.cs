namespace PropertyPortfolioManager.WebUI.Helpers
{
    public interface IPpmApiFacade
    {
        Task<int> PostAsync<TInput>(TInput obj, string relativePath);
        Task<TOutput> GetAsync<TOutput>(string relativePath) where TOutput : class;
    }
}
