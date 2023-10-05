using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Interfaces;
using PropertyPortfolioManager.WebUI.Models;
using System.Reflection;

namespace PropertyPortfolioManager.WebUI.Controllers
{
    public class UnitController : BaseController
    {
        private readonly IUnitService unitService;
        private readonly IUnitTypeService unitTypeService;

        public UnitController(IUserService userService, IPortfolioService portfolioService, IUnitTypeService unitTypeService, IUnitService unitService)
            : base(userService, portfolioService)
        {
            this.unitService = unitService;
            this.unitTypeService = unitTypeService;
        }


        [HttpGet]
        [Route("[controller]/{activeOnly?}")]
        public async Task<IActionResult> Index(bool activeOnly = true)
        {
            ViewBag.ActiveOnly = activeOnly;
            var units = await unitService.GetAll(activeOnly);
            return View(units);
        }

        public async Task<IActionResult> Details(int id)
        {
            var unit = await unitService.GetById(id);
            return View(unit);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new UnitEditViewModel()
            {
                UnitTypeList = await UserTypeSelectList()
            };

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

            var model = new UnitEditViewModel()
            {
                Unit = await unitService.GetById(id),
                UnitTypeList = await UserTypeSelectList()
            };

            return View(model);
        }

        private async Task<SelectList> UserTypeSelectList(bool activeOnly = true)
        {
            var unitTypeList = await unitTypeService.GetAll(activeOnly);

            return new SelectList(unitTypeList, "Id", "Type");
        }
    }
}
