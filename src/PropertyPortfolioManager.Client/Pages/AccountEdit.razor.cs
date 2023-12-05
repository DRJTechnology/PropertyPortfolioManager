using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Document;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
    public partial class AccountEdit
    {
        private IEnumerable<EntityTypeModel> accounttypes;

        [Inject]
        public IAccountTypeDataService accountTypeDataService { get; set; }

        [Inject]
        public IAccountDataService accountDataService { get; set; }

        [Inject]
        public IDocumentService documentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? AccountId { get; set; }

        private AccountEditModel AccountModel { get; set; } = new AccountEditModel();

        private bool Saved;
        private bool Initialising = true;
        private string Message = string.Empty;
        private string StatusClass = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Saved = false;

                int.TryParse(AccountId, out var accountId);

                accounttypes = await this.accountTypeDataService.GetAllAsync<EntityTypeModel>();

                if (accountId == 0) //new account is being created
                {
                    AccountModel = new AccountEditModel() {  Active = true };
                }
                else
                {
                    var account = await this.accountDataService.GetByIdAsync<AccountEditModel>(accountId);
                    AccountModel = account;
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

            if (AccountModel.Id == 0) //new
            {
                var addedAccount = await this.accountDataService.Create<AccountEditModel>(AccountModel);
                if (addedAccount != 0)
                {
                    StatusClass = "alert-success";
                    Message = "New account type added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new account type. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await this.accountDataService.Update<AccountEditModel>(AccountModel);
                StatusClass = "alert-success";
                Message = "Account type updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteAccount()
        {
            try
            {
                await this.accountDataService.DeleteAsync(AccountModel.Id);
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
            NavigationManager.NavigateTo("/account");
        }
    }
}
