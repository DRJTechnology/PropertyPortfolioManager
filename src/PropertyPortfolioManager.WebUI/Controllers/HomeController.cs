using DRJTechnology.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.WebUI.Interfaces;
using PropertyPortfolioManager.WebUI.Models;
using System.Diagnostics;

namespace PropertyPortfolioManager.WebUI.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICacheService cacheService;

        public HomeController(IUserService userService, IPortfolioService portfolioService, ILogger<HomeController> logger, ICacheService cacheService)
        : base(userService, portfolioService)
        {
            _logger = logger;
            this.cacheService = cacheService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> GetUser()
        {
            var value = await this.UserService.GetCurrent();

            return View(value);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}