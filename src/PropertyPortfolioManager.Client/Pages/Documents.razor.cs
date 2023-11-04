using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.Models.Model.Document;

namespace PropertyPortfolioManager.Client.Pages
{
    public partial class Documents
    {
        private List<DriveItemModel> DriveItemList = new List<DriveItemModel>();

        [Inject]
        public IDocumentService documentService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                DriveItemList = await this.documentService.GetCurrentFolderContentsAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
