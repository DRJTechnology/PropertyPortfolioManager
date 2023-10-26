using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly IContactService contactService;

        public ContactController(IUserService userService, IContactService contactService)
            : base(userService)
        {
            this.contactService = contactService;
        }

        [HttpGet]
        [Route("GetAll/{activeOnly}")]
        public async Task<List<ContactBasicResponseModel>> GetAll(bool activeOnly)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new List<ContactBasicResponseModel>();
            }
            else
            {
                return await this.contactService.GetAll((int)portfolioId, activeOnly);
            }
        }

        [HttpGet]
        [Route("GetById/{contactId}")]
        public async Task<ContactResponseModel> GetById(int contactId)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new ContactResponseModel();
            }
            else
            {
                return await this.contactService.GetById(contactId, (int)portfolioId);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<PpmApiResponse> Create(ContactEditModel contact)
        {
            try
            {
                var newContactId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Contact_Create: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    newContactId = await this.contactService.Create((await this.GetCurrentUser()).Id, (int)portfolioId, contact);
                }
                return new PpmApiResponse()
                {
                    CreatedId = newContactId,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<PpmApiResponse> Update(ContactEditModel contact)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Contact_Update: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.contactService.Update((await this.GetCurrentUser()).Id, (int)portfolioId, contact))
                    {
                        return new PpmApiResponse()
                        {
                            CreatedId = contact.Id,
                            Success = true,
                        };
                    }
                    else
                    {
                        return new PpmApiResponse()
                        {
                            Success = false,
                            ErrorMessage = $"ContactController: Failed to update contactId {contact.Id}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

    }
}
