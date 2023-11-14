using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Property;


namespace PropertyPortfolioManager.Client.Pages
{
    public partial class TenancyTypeEdit
    {
        [Inject]
        public ITenancyTypeDataService tenancyTypeDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? TenancyTypeId { get; set; }

        private EntityTypeModel TenancyType { get; set; } = new EntityTypeModel();

        private bool Saved;
        private string Message = string.Empty;
        private string StatusClass = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(TenancyTypeId, out var tenancyTypeId);

            if (tenancyTypeId == 0) //new tenancy type is being created
            {
                TenancyType = new EntityTypeModel() { Active = true };
            }
            else
            {
                TenancyType = await this.tenancyTypeDataService.GetByIdAsync<EntityTypeModel>(tenancyTypeId);
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (TenancyType.Id == 0) //new
            {
                var addedTenancyType = await this.tenancyTypeDataService.Create<EntityTypeModel>(TenancyType);
                if (addedTenancyType != null)
                {
                    StatusClass = "alert-success";
                    Message = "New tenancy type added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new tenancy type. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await this.tenancyTypeDataService.Update<EntityTypeModel>(TenancyType);
                StatusClass = "alert-success";
                Message = "Tenancy type updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteTenancyType()
        {
            try
            {
                await this.tenancyTypeDataService.DeleteAsync(TenancyType.Id);
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
            NavigationManager.NavigateTo("/tenancytype");
        }

    }
}
