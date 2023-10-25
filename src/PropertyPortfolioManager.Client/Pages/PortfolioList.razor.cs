using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Client.State;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class PortfolioList
    {
		private IEnumerable<PortfolioModel>? portfolios;

        [Inject]
        public IPortfolioDataService portfolioDataService { get; set; }

        [Inject]
        public ProfileState ProfileState { get; set; }

        public bool ActiveOnly { get; set; } = true;

        protected override async Task OnInitializedAsync()
		{
			await PopulatePortfolioListAsync();
		}

		public async Task IncludeInactive(bool includeInactive)
		{
            ActiveOnly = !includeInactive;
            await PopulatePortfolioListAsync();
        }

		public async Task SelectPortfolio(int portfolioId)
		{
            try
            {
                await this.portfolioDataService.SelectForCurrentUserAsync(portfolioId);
                ProfileState.CurrentPortfolio = portfolios.Where(p => p.Id == portfolioId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task PopulatePortfolioListAsync()
		{
			try
			{
                portfolios = await this.portfolioDataService.GetAllAsync<PortfolioModel>(ActiveOnly);
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
