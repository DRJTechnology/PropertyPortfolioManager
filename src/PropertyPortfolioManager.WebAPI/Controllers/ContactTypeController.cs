using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactTypeController : BaseController
    {
        private readonly IContactTypeService contactTypeService;

        public ContactTypeController(IUserService userService, IContactTypeService contactTypeService)
            : base(userService)
        {
            this.contactTypeService = contactTypeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<ContactTypeModel>> GetAll()
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if(portfolioId == null)
            {
                return new List<ContactTypeModel>();
            }
            else
            {
                return await this.contactTypeService.GetAll((int)portfolioId);
            }
        }

        [HttpGet]
        [Route("GetById/{contactTypeId}")]
        public async Task<ContactTypeModel> GetById(int contactTypeId)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new ContactTypeModel();
            }
            else
            {
                return await this.contactTypeService.GetById(contactTypeId, (int)portfolioId);
            }         
        }

        [HttpPost]
        [Route("Create")]
        public async Task<PpmApiResponse> Create(ContactTypeModel contactType)
        {
            try
            {
                var newContactTypeId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "ContactType_Create: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    newContactTypeId = await this.contactTypeService.Create((await this.GetCurrentUser()).Id, (int)portfolioId, contactType);
                }

                return new PpmApiResponse()
                {
                    CreatedId = newContactTypeId,
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
        public async Task<PpmApiResponse> Update(ContactTypeModel contactType)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "ContactType_Update: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.contactTypeService.Update((await this.GetCurrentUser()).Id, (int)portfolioId, contactType))
                    {
                        return new PpmApiResponse()
                        {
                            CreatedId = contactType.Id,
                            Success = true,
                        };
                    }
                    else
                    {
                        return new PpmApiResponse()
                        {
                            Success = false,
                            ErrorMessage = $"ContactTypeController: Failed to update contactTypeId {contactType.Id}"
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
