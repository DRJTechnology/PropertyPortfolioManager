using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Client.State;
using PropertyPortfolioManager.Models.Model.Property;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Pages
{
    public partial class Authentication
    {
        [Inject]
        public ProfileState ProfileState { get; set; }

        //[Inject]
        //public HttpClient Http { get; set; }
        [Inject]
        public IPortfolioDataService portfolioDataService { get; set; }


        [Parameter] public string? Action { get; set; }

        private async void UserAuthenticated()
        {
            try
            {
                var Portfolio = await this.portfolioDataService.GetCurrentAsync();
                if (Portfolio != null && Portfolio.Id != 0)
                {
                    ProfileState.CurrentPortfolio = Portfolio;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
