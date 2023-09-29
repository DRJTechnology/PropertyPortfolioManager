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
            var model = new UnitEditModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UnitEditModel unit)
        {
            var unitId = await unitService.Create(unit);
            return RedirectToAction("Details", new { id = unitId });
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var unit = unitService.GetById(id);
            return View(unit);
        }

        [HttpPost]
        public IActionResult Update(UnitEditModel unit)
        {
            var unitId = unitService.Update(unit);
            return RedirectToAction("Details", new { unitId });
        }
    }
}
