using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using PropertyPortfolioManager.WebUI.Models;
using System.Diagnostics;
using Microsoft.Identity.Abstractions;
using PropertyPortfolioManager.WebUI.Interfaces;
using DRJTechnology.Cache;

namespace PropertyPortfolioManager.WebUI.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICacheService cacheService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ICacheService cacheService)
        : base(userService)
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