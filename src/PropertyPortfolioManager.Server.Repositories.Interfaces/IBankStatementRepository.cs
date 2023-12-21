using PropertyPortfolioManager.Models.Model.Finance;
using System.Data;

namespace PropertyPortfolioManager.Server.Repositories.Interfaces
{
    public interface IBankStatementRepository
    {
        Task AddBankStatementRecords(int currentUserId, int accountId, DataTable recordList);
    }
}
