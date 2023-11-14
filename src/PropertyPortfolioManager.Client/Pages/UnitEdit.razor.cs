using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Graph.Models;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Document;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
    public partial class UnitEdit
    {
        private IEnumerable<EntityTypeModel> unittypes;

        [Inject]
        public IUnitTypeDataService unitTypeDataService { get; set; }

        [Inject]
        public IUnitDataService unitDataService { get; set; }

        [Inject]
        public IDocumentService documentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? UnitId { get; set; }

        private UnitEditModel UnitModel { get; set; } = new UnitEditModel();
        private bool DocumentSelectVisible = false;

        private bool Saved;
        private bool DataLoading = true;
        private string Message = string.Empty;
        private string StatusClass = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Saved = false;

                int.TryParse(UnitId, out var unitId);

                unittypes = await this.unitTypeDataService.GetAllAsync<EntityTypeModel>();

                if (unitId == 0) //new unit is being created
                {
                    UnitModel = new UnitEditModel() {  Active = true };
                }
                else
                {
                    var unit = await this.unitDataService.GetByIdAsync<UnitEditModel>(unitId);
                    UnitModel = unit;
                }
                DataLoading = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (UnitModel.Id == 0) //new
            {
                var addedUnit = await this.unitDataService.Create<UnitEditModel>(UnitModel);
                if (addedUnit != 0)
                {
                    StatusClass = "alert-success";
                    Message = "New unit type added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new unit type. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await this.unitDataService.Update<UnitEditModel>(UnitModel);
                StatusClass = "alert-success";
                Message = "Unit type updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteUnit()
        {
            try
            {
                await this.unitDataService.DeleteAsync(UnitModel.Id);
            }
            catch (Exception ex)
            {
                throw;
            }

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToList()
        {
            NavigationManager.NavigateTo("/unit");
        }

        protected void ShowImageSelection()
        {
            this.DocumentSelectVisible = true;
        }

        protected void HideImageSelection()
        {
            this.DocumentSelectVisible = false;
        }

        protected async void MainImageSelected(DriveItemModel driveItem)
        {
            UnitModel.MainPictureBase64 = await this.documentService.GetImageBase64FromDriveItemId(driveItem.Id);
            UnitModel.MainPicture = new FileModel() { ItemId = driveItem.Id, FileName = driveItem.Name, Size = driveItem.Size };
            this.DocumentSelectVisible = false;
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }
    }
}
