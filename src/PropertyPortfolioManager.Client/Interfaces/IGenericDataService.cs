namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IGenericDataService
    {
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(bool ActiveOnly = true);
        Task<TEntity> GetByIdAsync<TEntity>(int entityId);
        Task<int> Create<TEntity>(TEntity entity);
        Task<bool> Update<TEntity>(TEntity entity);
        Task<bool> DeleteAsync(int entityId);
    }
}
