using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Models.Model.Property;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class UnitTypeList
    {
		private UnitTypeModel[]? unittypes;

		[Inject]
        public HttpClient Http { get; set; }

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
                unittypes = await Http.GetFromJsonAsync<UnitTypeModel[]>($"api/UnitType/GetAll/{ActiveOnly}");
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
