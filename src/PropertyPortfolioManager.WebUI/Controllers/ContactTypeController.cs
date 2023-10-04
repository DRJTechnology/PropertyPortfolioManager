using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.Controllers
{
    public class ContactTypeController : BaseController
    {
        private readonly IContactTypeService contactTypeService;

        public ContactTypeController(IUserService userService, IPortfolioService portfolioService, IContactTypeService contactTypeService)
            : base(userService, portfolioService)
        {
            this.contactTypeService = contactTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var contactTypes = await contactTypeService.GetAll();
            return View(contactTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ContactTypeModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdate(ContactTypeModel contactType)
        {
            int contactTypeId = contactType.Id;

            if (contactType.Id == 0)
            {
                contactTypeId = await contactTypeService.Create(contactType);
            }
            else
            {
                await contactTypeService.Update(contactType);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await contactTypeService.GetById(id);

            return View(model);
        }

    }
}
