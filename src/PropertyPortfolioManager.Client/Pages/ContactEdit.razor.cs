using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
    public partial class ContactEdit
    {
        private IEnumerable<ContactTypeModel> contacttypes;

        [Inject]
        public IContactTypeDataService contactTypeDataService { get; set; }

        [Inject]
        public IContactDataService contactDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? ContactId { get; set; }

        private ContactEditModel ContactModel { get; set; } = new ContactEditModel();


        private bool Saved;
        private bool Initialising = true;
        private string Message = string.Empty;
        private string StatusClass = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Saved = false;

                int.TryParse(ContactId, out var contactId);

                contacttypes = await this.contactTypeDataService.GetAllAsync<ContactTypeModel>();

                if (contactId == 0) //new contact is being created
                {
                    ContactModel = new ContactEditModel() {  Active = true };
                }
                else
                {
                    var contact = await this.contactDataService.GetByIdAsync<ContactEditModel>(contactId);
                    ContactModel = contact;
                }
                Initialising = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (ContactModel.Id == 0) //new
            {
                var addedContact = await this.contactDataService.Create<ContactEditModel>(ContactModel);
                if (addedContact != 0)
                {
                    StatusClass = "alert-success";
                    Message = "New contact type added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new contact type. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await this.contactDataService.Update<ContactEditModel>(ContactModel);
                StatusClass = "alert-success";
                Message = "Contact type updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteContact()
        {
            try
            {
                await this.contactDataService.DeleteAsync(ContactModel.Id);
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
            NavigationManager.NavigateTo("/contact");
        }

    }
}
