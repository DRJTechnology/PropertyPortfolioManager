using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Models.Model.General;
using System.Net.Http.Json;


namespace PropertyPortfolioManager.Client.Pages
{
    public partial class ContactTypeEdit
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? ContactTypeId { get; set; }

        private ContactTypeModel ContactType { get; set; } = new ContactTypeModel();

        private bool Saved;
        private string Message = string.Empty;
        private string StatusClass = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(ContactTypeId, out var contactTypeId);

            if (contactTypeId == 0) //new contact type is being created
            {
                ContactType = new ContactTypeModel() { Active = true };
            }
            else
            {
                ContactType = await Http.GetFromJsonAsync<ContactTypeModel>($"api/ContactType/GetById/{ContactTypeId}");
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (ContactType.Id == 0) //new
            {
                var addedContactType = await Http.PostAsJsonAsync<ContactTypeModel>("api/ContactType/Create", ContactType);
                if (addedContactType != null)
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
                await Http.PostAsJsonAsync<ContactTypeModel>("api/ContactType/Update", ContactType);
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

        protected async Task DeleteContactType()
        {
            try
            {
                await Http.DeleteAsync($"api/Portfolio/Delete/{ContactTypeId}");
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
            NavigationManager.NavigateTo("/contacttype");
        }

    }
}
