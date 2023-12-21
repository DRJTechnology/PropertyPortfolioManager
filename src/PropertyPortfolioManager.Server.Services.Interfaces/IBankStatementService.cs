using PropertyPortfolioManager.Models.Model.Document;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IBankStatementService
    {
        Task UploadBankStatement(int currentUserId, int portfolioId, Stream stream);
    }
}
