using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.Controllers
{
    public class UnitTypeController : BaseController
    {
        private readonly IUnitTypeService unitTypeService;

        public UnitTypeController(IUserService userService, IPortfolioService portfolioService, IUnitTypeService unitTypeService)
            : base(userService, portfolioService)
        {
            this.unitTypeService = unitTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var unitTypes = await unitTypeService.GetAll();
            return View(unitTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new UnitTypeModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdate(UnitTypeModel unitType)
        {
            int unitTypeId = unitType.Id;

            if (unitType.Id == 0)
            {
                unitTypeId = await unitTypeService.Create(unitType);
            }
            else
            {
                await unitTypeService.Update(unitType);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await unitTypeService.GetById(id);

            return View(model);
        }

    }
}
