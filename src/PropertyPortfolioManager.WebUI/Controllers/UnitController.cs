using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Interfaces;
using System.Reflection;

namespace PropertyPortfolioManager.WebUI.Controllers
{
    public class UnitController : BaseController
    {
        private readonly IUnitService unitService;
        public UnitController(IUserService userService, IUnitService unitService)
            : base(userService)
        {
            this.unitService = unitService;
        }

        public async Task<IActionResult> Index()
        {
            var units = await unitService.GetAll();
            return View(units);
        }

        public async Task<IActionResult> Details(int id)
        {
            var unit = await unitService.GetById(id);
            return View(unit);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new UnitResponseModel();
            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdate(UnitEditModel unit)
        {
            int unitId = unit.Id;

            if (unit.Id == 0)
            {
                unitId = await unitService.Create(unit);
            }
            else
            {
                await unitService.Update(unit);
            }

            return RedirectToAction("Details", new { id = unitId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var unit = await unitService.GetById(id);
            return View(unit);
        }
    }
}
