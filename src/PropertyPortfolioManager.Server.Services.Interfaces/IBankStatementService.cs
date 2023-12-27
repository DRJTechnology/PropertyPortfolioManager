namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IBankStatementService
    {
        Task<string> UploadBankStatement(int currentUserId, int portfolioId, Stream stream);
    }
}
