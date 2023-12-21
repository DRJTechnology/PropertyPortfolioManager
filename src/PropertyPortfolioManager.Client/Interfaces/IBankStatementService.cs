using PropertyPortfolioManager.Models.Model.Document;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IBankStatementService : IGenericDataService
    {
        Task UploadBankStatement(Stream content, string filename);
    }
}
