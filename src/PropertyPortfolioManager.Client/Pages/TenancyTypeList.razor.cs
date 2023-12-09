using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class TenancyTypeList
    {
		private IEnumerable<EntityTypeModel> tenancytypes;

        [Inject]
        public ITenancyTypeDataService tenancyTypeDataService { get; set; }

        public bool ActiveOnly { get; set; } = true;

        protected override async Task OnInitializedAsync()
		{
			await PopulateTenancyTypeListAsync();
		}

		public async Task IncludeInactive(bool includeInactive)
		{
            ActiveOnly = !includeInactive;
            await PopulateTenancyTypeListAsync();
        }

		private async Task PopulateTenancyTypeListAsync()
		{
			try
			{
                tenancytypes = await this.tenancyTypeDataService.GetAllAsync<EntityTypeModel>(ActiveOnly);
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
