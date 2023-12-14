using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactTypeController : BaseController
    {
        private readonly ILogger<ContactTypeController> logger;
        private readonly IContactTypeService contactTypeService;

        public ContactTypeController(ILogger<ContactTypeController> logger, IUserService userService, IContactTypeService contactTypeService)
            : base(userService)
        {
            this.logger = logger;
            this.contactTypeService = contactTypeService;
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
                    return this.Ok(new List<ContactTypeModel>());
                }
                else
                {
                    var returnVal = await this.contactTypeService.GetAll((int)portfolioId, activeOnly);
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
        [Route("GetById/{contactTypeId}")]
        public async Task<IActionResult> GetById(int contactTypeId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return this.Ok(new ContactTypeModel());
                }
                else
                {
                    var returnVal = await this.contactTypeService.GetById(contactTypeId, (int)portfolioId);
                    return this.Ok(returnVal);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetById{contactTypeId}");
                return this.BadRequest();
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
                logger.LogError(ex, $"Update");
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        [HttpDelete]
        [Route("Delete/{contactTypeId}")]
        public async Task<PpmApiResponse> Delete(int contactTypeId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "ContactType_Delete: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.contactTypeService.Delete((await this.GetCurrentUser()).Id, (int)portfolioId, contactTypeId))
                    {
                        return new PpmApiResponse()
                        {
                            Success = true,
                        };
                    }
                    else
                    {
                        return new PpmApiResponse()
                        {
                            Success = false,
                            ErrorMessage = $"ContactTypeController: Failed to delete contactTypeId {contactTypeId}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete/{contactTypeId}");
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
