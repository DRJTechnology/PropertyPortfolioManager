using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class UnitTypeList
    {
		private IEnumerable<UnitTypeModel> unittypes;

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
                //unittypes = await Http.GetFromJsonAsync<UnitTypeModel[]>($"api/UnitType/GetAll/{ActiveOnly}");
                unittypes = await this.unitTypeDataService.GetAllAsync<UnitTypeModel>(ActiveOnly);
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
