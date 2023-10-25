using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class UnitList
    {
		private IEnumerable<UnitBasicResponseModel>? units;

        [Inject]
        public IUnitDataService unitDataService { get; set; }

        public bool ActiveOnly { get; set; } = true;

        protected override async Task OnInitializedAsync()
		{
			await PopulateUnitListAsync();
		}

		public async Task IncludeInactive(bool includeInactive)
		{
            ActiveOnly = !includeInactive;
            await PopulateUnitListAsync();
        }

		private async Task PopulateUnitListAsync()
		{
			try
			{
                units = await this.unitDataService.GetAllAsync<UnitBasicResponseModel>(ActiveOnly);
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
