namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IGenericDataService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool ActiveOnly = true);
        Task<TEntity> GetByIdAsync(int entityId);
        Task<int> Create(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> DeleteAsync(int entityId);
    }
}
