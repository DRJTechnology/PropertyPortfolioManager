using PropertyPortfolioManager.Client.Interfaces;

namespace PropertyPortfolioManager.Client.Services
{
    public class BankStatementService : GenericDataService, IBankStatementService
    {
        public BankStatementService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "BankStatement";
        }

        public async Task UploadBankStatement(Stream content, string filename)
        {
            var formContent = new MultipartFormDataContent();
            formContent.Add(new StreamContent(content), "File", filename);

            var response = await httpClient.PostAsync($"api/BankStatement/UploadBankStatement", formContent);

            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to upload bank statement.");
            }
        }
    }
}
