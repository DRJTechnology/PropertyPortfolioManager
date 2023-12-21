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

        private async Task HandleFileSelection(InputFileChangeEventArgs e)
        {
            file = e.File;
            var stream = file.OpenReadStream();
            await bankStatementService.UploadBankStatement(stream, file.Name);
        }
    }
}
