using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Enums;
using PropertyPortfolioManager.Models.Model.Finance;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class BankAccounts
    {
		private IEnumerable<TransactionDetailResponseModel> transactionDetails;
        private DateTime? fromDate;
        private DateTime? toDate;
        private int accountId;
        private int transactionTypeId;
        private IEnumerable<AccountResponseModel> accounts;
        private bool Initialising = true;

        [Inject]
        public ITransactionDetailDataService transactionDetailDataService { get; set; }

        [Inject]
        public IAccountDataService accountDataService { get; set; }

        [Inject]
        public ITransactionTypeDataService TransactionTypeDataService { get; set; }

        protected override async Task OnInitializedAsync()
		{
            fromDate = DateTime.Today.AddMonths(-1);
            await PopulateAccountsAsync();
            await PopulateTransactionListAsync();
            Initialising = false;
        }

        private async Task PopulateAccountsAsync()
        {
            try
            {
                accounts = await this.accountDataService.GetByTypeAsync(AccountType.BankAccount, true);
                if (accounts != null) { 
                    accountId = accounts.First().Id;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task PopulateTransactionListAsync()
        {
            try
            {
                transactionDetails = await this.transactionDetailDataService.GetAsync(fromDate, toDate, accountId, transactionTypeId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
