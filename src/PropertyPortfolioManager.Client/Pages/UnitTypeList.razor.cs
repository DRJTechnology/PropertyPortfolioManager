using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class UnitTypeList
    {
		private IEnumerable<EntityTypeModel> unittypes;

        [Inject]
        public IUnitTypeDataService unitTypeDataService { get; set; }

        public bool ActiveOnly { get; set; } = true;

        protected override async Task OnInitializedAsync()
		{
			await PopulateUnitTypeListAsync();
		}

		public async Task IncludeInactive(bool includeInactive)
		{
            ActiveOnly = !includeInactive;
            await PopulateUnitTypeListAsync();
        }

		private async Task PopulateUnitTypeListAsync()
		{
			try
			{
                unittypes = await this.unitTypeDataService.GetAllAsync<EntityTypeModel>(ActiveOnly);
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
