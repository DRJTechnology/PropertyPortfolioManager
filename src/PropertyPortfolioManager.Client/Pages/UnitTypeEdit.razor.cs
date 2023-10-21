using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Models.Model.Property;
using System.Net.Http.Json;


namespace PropertyPortfolioManager.Client.Pages
{
    public partial class UnitTypeEdit
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? UnitTypeId { get; set; }

        private UnitTypeModel UnitType { get; set; } = new UnitTypeModel();

        private bool Saved;
        private string Message = string.Empty;
        private string StatusClass = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(UnitTypeId, out var unitTypeId);

            if (unitTypeId == 0) //new unit type is being created
            {
                UnitType = new UnitTypeModel() { Active = true };
            }
            else
            {
                UnitType = await Http.GetFromJsonAsync<UnitTypeModel>($"api/UnitType/GetById/{UnitTypeId}");
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (UnitType.Id == 0) //new
            {
                var addedUnitType = await Http.PostAsJsonAsync<UnitTypeModel>("api/UnitType/Create", UnitType);
                if (addedUnitType != null)
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
                await Http.PostAsJsonAsync<UnitTypeModel>("api/UnitType/Update", UnitType);
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

        protected async Task DeleteUnitType()
        {
            try
            {
                await Http.DeleteAsync($"api/UnitType/Delete/{UnitTypeId}");
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
            NavigationManager.NavigateTo("/unittype");
        }

    }
}
