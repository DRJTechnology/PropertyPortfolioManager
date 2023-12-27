using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PropertyPortfolioManager.Client.Interfaces;

namespace PropertyPortfolioManager.Client.Pages
{
    public partial class UploadBankStatement
    {
        [Inject]
        public IBankStatementService bankStatementService { get; set; }

        private IBrowserFile file;
        private string uploadResponse = string.Empty;
        private bool uploading { get; set; } = false;

        private async Task HandleFileSelection(InputFileChangeEventArgs e)
        {
            uploading = true;
            file = e.File;
            var stream = file.OpenReadStream();
            var response = await bankStatementService.UploadBankStatement(stream, file.Name);
            uploadResponse = response.Replace("\r\n", "<br />");
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            uploading = false;
        }
    }
}
