using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly ILogger<ContactController> logger;
        private readonly IContactService contactService;

        public ContactController(ILogger<ContactController> logger, IUserService userService, IContactService contactService)
            : base(userService)
        {
            this.logger = logger;
            this.contactService = contactService;
        }

        [HttpGet]
        [Route("GetAll/{activeOnly}")]
        public async Task<IActionResult> GetAll(bool activeOnly)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return this.Ok(new List<ContactBasicResponseModel>());
                }
                else
                {
                    var returnVal = await this.contactService.GetAll((int)portfolioId, activeOnly);
                    return this.Ok(returnVal);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetAll");
                return this.BadRequest();
            }

        }

        [HttpGet]
        [Route("GetById/{contactId}")]
        public async Task<IActionResult> GetById(int contactId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return this.Ok(new ContactResponseModel());
                }
                else
                {
                    var contact = await this.contactService.GetById(contactId, (int)portfolioId);
                    return this.Ok(contact);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetById{contactId}");
                return this.BadRequest();
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
                logger.LogError(ex, $"Create");
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
                logger.LogError(ex, $"Update");
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

    }
}
