using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Models.Model.Property;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class UnitList
    {
		private UnitBasicResponseModel[]? units;

		[Inject]
        public HttpClient Http { get; set; }

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
                units = await Http.GetFromJsonAsync<UnitBasicResponseModel[]>($"api/Unit/GetAll/{ActiveOnly}");
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
