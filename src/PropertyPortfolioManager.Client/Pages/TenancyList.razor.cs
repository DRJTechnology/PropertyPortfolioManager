using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class TenancyList
    {
		private IEnumerable<TenancyResponseModel> tenancies;

        [Inject]
        public ITenancyDataService tenancyDataService { get; set; }

        public bool ActiveOnly { get; set; } = true;

        protected override async Task OnInitializedAsync()
		{
			await PopulateTenancyListAsync();
		}

        public async Task IncludeInactive(bool includeInactive)
        {
            ActiveOnly = !includeInactive;
            await PopulateTenancyListAsync();
        }

        private async Task PopulateTenancyListAsync()
		{
			try
			{
                tenancies = await this.tenancyDataService.GetAllAsync<TenancyResponseModel>(ActiveOnly);
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
