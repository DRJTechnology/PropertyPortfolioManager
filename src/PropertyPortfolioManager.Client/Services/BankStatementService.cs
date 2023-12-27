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

        public async Task<string> UploadBankStatement(Stream content, string filename)
        {
            var formContent = new MultipartFormDataContent();
            formContent.Add(new StreamContent(content), "File", filename);

            var response = await httpClient.PostAsync($"api/BankStatement/UploadBankStatement", formContent);

            var returnVal = string.Empty;

            if (response == null || !response.IsSuccessStatusCode)
            {
                returnVal = "Error - upload of bank statement failed.";
            }

            returnVal = await response.Content.ReadAsStringAsync();
            return returnVal;
        }
    }
}
