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
        public async Task<IActionResult> Index()
        {
            var portfolios = await PortfolioService.GetAll();
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

    }
}
