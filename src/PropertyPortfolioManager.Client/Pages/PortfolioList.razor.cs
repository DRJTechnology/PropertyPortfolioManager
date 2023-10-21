using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PropertyPortfolioManager.Client.State;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Shared;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace PropertyPortfolioManager.Client.Pages
{
	[Authorize]
	public partial class PortfolioList
    {
		private PortfolioModel[]? portfolios;

		[Inject]
        public HttpClient Http { get; set; }

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
                await Http.GetFromJsonAsync<PortfolioModel>($"api/Portfolio/SelectForCurrentUser/{portfolioId}");
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
                portfolios = await Http.GetFromJsonAsync<PortfolioModel[]>($"api/Portfolio/GetAll/{ActiveOnly}");
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
