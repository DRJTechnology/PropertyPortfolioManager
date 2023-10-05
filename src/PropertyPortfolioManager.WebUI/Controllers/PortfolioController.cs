using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.Controllers
{
    public class PortfolioController : BaseController
    {
        public PortfolioController(IUserService userService, IPortfolioService portfolioService)
            : base(userService, portfolioService)
        {
        }

        [HttpGet]
        [Route("[controller]/{activeOnly?}")]
        public async Task<IActionResult> Index(bool activeOnly = true)
        {
            ViewBag.ActiveOnly = activeOnly;
            var portfolios = await PortfolioService.GetAll(activeOnly);
            return View(portfolios);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new PortfolioModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdate(PortfolioModel unitType)
        {
            int unitId = unitType.Id;

            if (unitType.Id == 0)
            {
                unitId = await PortfolioService.Create(unitType);
            }
            else
            {
                await PortfolioService.Update(unitType);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await PortfolioService.GetById(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SelectPortfolio(int id)
        {
            await PortfolioService.SelectForCurrentUser(id);
            return RedirectToAction("Index", "Home");
        }

    }
}
