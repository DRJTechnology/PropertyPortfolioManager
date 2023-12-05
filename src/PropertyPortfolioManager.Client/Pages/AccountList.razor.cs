using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Finance;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class AccountList
    {
		private IEnumerable<AccountResponseModel>? accounts;

        [Inject]
        public IAccountDataService accountDataService { get; set; }

        public bool ActiveOnly { get; set; } = true;

        protected override async Task OnInitializedAsync()
		{
			await PopulateAccountListAsync();
		}

		public async Task IncludeInactive(bool includeInactive)
		{
            ActiveOnly = !includeInactive;
            await PopulateAccountListAsync();
        }

		private async Task PopulateAccountListAsync()
		{
			try
			{
                accounts = await this.accountDataService.GetAllAsync<AccountResponseModel>(ActiveOnly);
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
