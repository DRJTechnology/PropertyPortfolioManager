using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
    public partial class TenancyEdit
    {
        private IEnumerable<EntityTypeModel> tenancytypes;

        [Inject]
        public ITenancyTypeDataService tenancyTypeDataService { get; set; }

        [Inject]
        public ITenancyDataService tenancyDataService { get; set; }

        [Inject]
        public IDocumentService documentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? TenancyId { get; set; }

        private TenancyEditModel TenancyModel { get; set; } = new TenancyEditModel();

        private bool Saved;
        private bool Initialising = true;
        private string Message = string.Empty;
        private string StatusClass = string.Empty;
        private bool ContactSelectVisible = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Saved = false;

                int.TryParse(TenancyId, out var tenancyId);

                tenancytypes = await this.tenancyTypeDataService.GetAllAsync<EntityTypeModel>();

                if (tenancyId == 0) //new tenancy is being created
                {
                    TenancyModel = new TenancyEditModel() {  Active = true };
                }
                else
                {
                    var tenancy = await this.tenancyDataService.GetByIdAsync<TenancyEditModel>(tenancyId);
                    TenancyModel = tenancy;
                }
                Initialising = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ContactSelected(int selectedContactId)
        {
            if (selectedContactId > 0)
            {
                AddContact(selectedContactId);
            }
        }

        private async void AddContact(int selectedContactId)
        {
            var contact = await this.tenancyDataService.AddContact(new TenancyContactModel() { TenancyId = TenancyModel.Id, ContactId = selectedContactId } );
            if (!TenancyModel.Contacts.Exists(c => c.Id == contact.Id))
            {
                TenancyModel.Contacts.Add(contact);
            }
            ContactSelectVisible = false;
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        private async void RemoveContact(int selectedContactId)
        {
            if (await this.tenancyDataService.RemoveContact(new TenancyContactModel() { TenancyId = TenancyModel.Id, ContactId = selectedContactId }))
            {
                TenancyModel.Contacts.Remove(TenancyModel.Contacts.Where(c => c.Id == selectedContactId).FirstOrDefault());
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (TenancyModel.Id == 0) //new
            {
                var addedTenancy = await this.tenancyDataService.Create<TenancyEditModel>(TenancyModel);
                if (addedTenancy != 0)
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
                await this.tenancyDataService.Update<TenancyEditModel>(TenancyModel);
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

        protected async Task DeleteTenancy()
        {
            try
            {
                await this.tenancyDataService.DeleteAsync(TenancyModel.Id);
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
            NavigationManager.NavigateTo("/tenancy");
        }

        protected void ShowContactSelection()
        {
            this.ContactSelectVisible = true;
        }

        protected void HideContactSelection()
        {
            this.ContactSelectVisible = false;
        }

    }
}
