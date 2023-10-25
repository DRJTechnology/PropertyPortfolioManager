using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Client.State;
using PropertyPortfolioManager.Models.Model.Property;
using System.Net.Http.Json;


namespace PropertyPortfolioManager.Client.Pages
{
    public partial class PortfolioEdit
    {
        [Inject]
        public ProfileState ProfileState { get; set; }

        [Inject]
        public IPortfolioDataService portfolioDataService { get; set; }

        //[Inject]
        //public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        //public int PortfolioId { get; set; }
        public string? PortfolioId { get; set; }

        private PortfolioModel Portfolio { get; set; } = new PortfolioModel();

        private bool Saved;
        private string Message = string.Empty;
        private string StatusClass = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(PortfolioId, out var portfolioId);

            if (portfolioId == 0) //new portfolio is being created
            {
                Portfolio = new PortfolioModel() { Active = true };
            }
            else
            {
                //Portfolio = await Http.GetFromJsonAsync<PortfolioModel>($"api/Portfolio/GetById/{PortfolioId}");
                Portfolio = await this.portfolioDataService.GetByIdAsync<PortfolioModel>(portfolioId);
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (Portfolio.Id == 0) //new
            {
                //var addedPortfolio = await Http.PostAsJsonAsync<PortfolioModel>("api/Portfolio/Create", Portfolio);
                var addedPortfolioId = await this.portfolioDataService.Create<PortfolioModel>(Portfolio);
                if (addedPortfolioId != 0)
                {
                    StatusClass = "alert-success";
                    Message = "New portfolio added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new portfolio. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                //await Http.PostAsJsonAsync<PortfolioModel>("api/Portfolio/Update", Portfolio);
                await this.portfolioDataService.Update<PortfolioModel>(Portfolio);
                StatusClass = "alert-success";
                Message = "Portfolio updated successfully.";
                Saved = true;

                if (ProfileState.CurrentPortfolio != null && ProfileState.CurrentPortfolio.Id == Portfolio.Id)
                {
                    ProfileState.CurrentPortfolio = Portfolio;
                }
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeletePortfolio()
        {
            try
            {
                //await Http.DeleteAsync($"api/Portfolio/Delete/{PortfolioId}");
                await this.portfolioDataService.DeleteAsync(Portfolio.Id);
            }
            catch (Exception ex)
            {
                throw;
            }

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToList()
        {
            NavigationManager.NavigateTo("/portfolio");
        }

    }
}
