using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.State;
using PropertyPortfolioManager.Models.Model.Property;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Pages
{
    public partial class Authentication
    {
        [Inject]
        public ProfileState ProfileState { get; set; }

        [Inject]
        public HttpClient Http { get; set; }


        [Parameter] public string? Action { get; set; }

        private async void UserAuthenticated()
        {
            try
            {
                var Portfolio = await Http.GetFromJsonAsync<PortfolioModel>($"api/Portfolio/GetById/{2}");
                ProfileState.CurrentPortfolio = Portfolio;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
