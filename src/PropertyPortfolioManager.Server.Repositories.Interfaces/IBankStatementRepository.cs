using PropertyPortfolioManager.Models.Dto.Finance;
using System.Data;

namespace PropertyPortfolioManager.Server.Repositories.Interfaces
{
    public interface IBankStatementRepository
    {
        Task<UploadResultDto> AddBankStatementRecords(int currentUserId, int portfolioId, int bankAccountId, DataTable recordList);
    }
}
