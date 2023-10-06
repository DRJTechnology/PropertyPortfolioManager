using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.WebUI.Interfaces;
using PropertyPortfolioManager.WebUI.Models;

namespace PropertyPortfolioManager.WebUI.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService contactService;
        private readonly IContactTypeService contactTypeService;

        public ContactController(IUserService userService, IPortfolioService portfolioService, IContactTypeService contactTypeService, IContactService contactService)
            : base(userService, portfolioService)
        {
            this.contactService = contactService;
            this.contactTypeService = contactTypeService;
        }


        [HttpGet]
        [Route("[controller]/List/{activeOnly?}")]
        public async Task<IActionResult> Index(bool activeOnly = true)
        {
            ViewBag.ActiveOnly = activeOnly;
            var contacts = await contactService.GetAll(activeOnly);
            return View(contacts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contact = await contactService.GetById(id);
            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ContactEditViewModel()
            {
                ContactTypeList = await ContactTypeSelectList()
            };

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdate(ContactEditModel contact)
        {
            int contactId = contact.Id;

            if (contact.Id == 0)
            {
                contactId = await contactService.Create(contact);
            }
            else
            {
                await contactService.Update(contact);
            }

            return RedirectToAction("Details", new { id = contactId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var model = new ContactEditViewModel()
            {
                Contact = await contactService.GetById(id),
                ContactTypeList = await ContactTypeSelectList()
            };

            return View(model);
        }

        private async Task<SelectList> ContactTypeSelectList(bool activeOnly = true)
        {
            var contactTypeList = await contactTypeService.GetAll(activeOnly);

            return new SelectList(contactTypeList, "Id", "Type");
        }
    }
}
