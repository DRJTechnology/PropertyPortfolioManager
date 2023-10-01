using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly IPortfolioService portfolioService;

        public NavigationViewComponent(IPortfolioService portfolioService)
        {
            this.portfolioService = portfolioService;
        }

        //public IViewComponentResult Invoke()
        //{
        //    return View();
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var portfolio = await this.portfolioService.GetCurrent();
            return View(portfolio);
        }
    }
}
