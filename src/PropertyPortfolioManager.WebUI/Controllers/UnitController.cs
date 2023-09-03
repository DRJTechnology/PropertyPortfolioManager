using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Interfaces;

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

        public IActionResult Index()
        {
            var units = unitService.GetAll();
            return View(units);
        }

        public IActionResult Details(int unitId)
        {
            var unit = unitService.GetById(unitId);
            return View(unit);
        }

        [HttpPost]
        public IActionResult Create(UnitEditModel unit)
        {
            var unitId = unitService.Create(unit);
            return RedirectToAction("Details", new { unitId = unitId });
        }

        [HttpPost]
        public IActionResult Update(UnitEditModel unit)
        {
            var unitId = unitService.Update(unit);
            return RedirectToAction("Details", new { unitId = unitId });
        }
    }
}
