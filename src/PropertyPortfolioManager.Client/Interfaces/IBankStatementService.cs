using PropertyPortfolioManager.Models.Model.Document;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IBankStatementService : IGenericDataService
    {
        Task<string> UploadBankStatement(Stream content, string filename);
    }
}
