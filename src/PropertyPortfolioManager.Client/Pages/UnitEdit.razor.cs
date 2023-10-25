using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
    public partial class UnitEdit
    {
        private IEnumerable<UnitTypeModel> unittypes;

        [Inject]
        public IUnitTypeDataService unitTypeDataService { get; set; }

        [Inject]
        public IUnitDataService unitDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? UnitId { get; set; }

        private UnitEditModel UnitModel { get; set; } = new UnitEditModel();


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

                //UnitModel.UnitTypeId = 1;

                unittypes = await this.unitTypeDataService.GetAllAsync<UnitTypeModel>();
                //unittypes = new List<UnitTypeModel>()
                //{
                //    new UnitTypeModel() { Id = 1, Type = "One", Active = true, PortfolioId = 2 },
                //    new UnitTypeModel() { Id = 2, Type = "Two", Active = true, PortfolioId = 2 },
                //    new UnitTypeModel() { Id = 3, Type = "Three", Active = true, PortfolioId = 2 },
                //    new UnitTypeModel() { Id = 4, Type = "Four", Active = true, PortfolioId = 2 },
                //    new UnitTypeModel() { Id = 5, Type = "Five", Active = true, PortfolioId = 2 },
                //    new UnitTypeModel() { Id = 6, Type = "Six", Active = true, PortfolioId = 2 },
                //    new UnitTypeModel() { Id = 7, Type = "Seven", Active = true, PortfolioId = 2 },
                //};

                if (unitId == 0) //new unit is being created
                {
                    UnitModel = new UnitEditModel() {  Active = true };
                }
                else
                {
                    var unit = await this.unitDataService.GetByIdAsync<UnitEditModel>(unitId);
                    //var unit = new UnitEditModel();
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

    }
}
