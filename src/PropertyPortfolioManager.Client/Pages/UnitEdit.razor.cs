using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Models.Model.Property;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Pages
{
    public partial class UnitEdit
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? UnitId { get; set; }

        private UnitEditModel UnitModel { get; set; } = new UnitEditModel();
        private UnitTypeModel[]? unittypes;

        private bool Saved;
        private string Message = string.Empty;
        private string StatusClass = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(UnitId, out var unitId);
            unittypes = await Http.GetFromJsonAsync<UnitTypeModel[]>($"api/UnitType/GetAll");

            if (unitId == 0) //new unit is being created
            {
                UnitModel = new UnitEditModel() {  Active = true };
            }
            else
            {
                var unit = await Http.GetFromJsonAsync<UnitResponseModel>($"api/Unit/GetById/{UnitId}");
                UnitModel = unit;
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (UnitModel.Id == 0) //new
            {
                var addedUnit = await Http.PostAsJsonAsync<UnitEditModel>("api/Unit/Create", UnitModel);
                if (addedUnit != null)
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
                await Http.PostAsJsonAsync<UnitEditModel>("api/Unit/Update", UnitModel);
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
                await Http.DeleteAsync($"api/Unit/Delete/{UnitId}");
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
