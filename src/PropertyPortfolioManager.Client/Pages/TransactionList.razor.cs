using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class TransactionList
    {
		private IEnumerable<TransactionDetailResponseModel> transactionDetails;
        private DateTime? fromDate;
        private DateTime? toDate;
        private int accountId;
        private int transactionTypeId;
        private IEnumerable<AccountResponseModel> accounts;
        private IEnumerable<EntityTypeBasicModel> transactiontypes;
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
            await PopulateTransactionTypesAsync();
            await PopulateAccountsAsync();
            await PopulateTransactionListAsync();
            Initialising = false;
        }

        private async Task PopulateAccountsAsync()
        {
            try
            {
                accounts = await this.accountDataService.GetAllAsync<AccountResponseModel>(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task PopulateTransactionTypesAsync()
        {
            try
            {
                transactiontypes = await this.TransactionTypeDataService.GetAllAsync();
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
