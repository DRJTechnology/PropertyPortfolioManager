using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenancyController : BaseController
    {
        private readonly ILogger<TenancyController> logger;
        private readonly ITenancyService tenancyService;
        private readonly IContactService contactService;

        public TenancyController(ILogger<TenancyController> logger, IUserService userService, ITenancyService tenancyService, IContactService contactService)
            : base(userService)
        {
            this.logger = logger;
            this.tenancyService = tenancyService;
            this.contactService = contactService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return await this.GetAll(true);
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
                    return this.Ok(new List<TenancyResponseModel>());
                }
                else
                {
                    var returnVal = await this.tenancyService.GetAll((int)portfolioId, activeOnly);
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
        [Route("GetById/{tenancyId}")]
        public async Task<IActionResult> GetById(int tenancyId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return this.Ok(new TenancyResponseModel());
                }
                else
                {
                    var portfolio = await this.tenancyService.GetById(tenancyId, (int)portfolioId);
                    return this.Ok(portfolio);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetById{tenancyId}");
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<PpmApiResponse> Create(TenancyEditModel tenancy)
        {
            try
            {
                var newTenancyId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Tenancy_Create: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    newTenancyId = await this.tenancyService.Create((await this.GetCurrentUser()).Id, (int)portfolioId, tenancy);
                }
                return new PpmApiResponse()
                {
                    CreatedId = newTenancyId,
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
        public async Task<PpmApiResponse> Update(TenancyEditModel tenancy)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Tenancy_Update: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.tenancyService.Update((await this.GetCurrentUser()).Id, (int)portfolioId, tenancy))
                    {
                        return new PpmApiResponse()
                        {
                            CreatedId = tenancy.Id,
                            Success = true,
                        };
                    }
                    else
                    {
                        return new PpmApiResponse()
                        {
                            Success = false,
                            ErrorMessage = $"TenancyController: Failed to update tenancyId {tenancy.Id}"
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
        [Route("Delete/{tenancyId}")]
        public async Task<PpmApiResponse> Delete(int tenancyId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "TenancyType_Delete: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.tenancyService.Delete((await this.GetCurrentUser()).Id, (int)portfolioId, tenancyId))
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
                            ErrorMessage = $"TenancyTypeController: Failed to delete tenancyTypeId {tenancyId}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete/{tenancyId}");
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        [HttpPost]
        [Route("AddContact")]
        public async Task<IActionResult> AddContact(TenancyContactModel tenancyContact)
        {
            try
            {
                var newTenancyContactId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    throw new Exception("Tenancy_AddContact: User has no Selected Portfolio Id set.");
                }
                else
                {
                    newTenancyContactId = await this.tenancyService.AddContact((await this.GetCurrentUser()).Id, (int)portfolioId, tenancyContact);
                }

                var returnVal = await this.contactService.GetById(tenancyContact.ContactId, (int)portfolioId);
                return this.Ok(returnVal);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"AddContact");
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("RemoveContact")]
        public async Task<PpmApiResponse> RemoveContact(TenancyContactModel tenancyContact)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Tenancy_AddContact: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.tenancyService.RemoveContact((await this.GetCurrentUser()).Id, (int)portfolioId, tenancyContact))
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
                            ErrorMessage = $"TenancyController: Failed to remove contact ({tenancyContact.ContactId}) from tenancy ({tenancyContact.TenancyId})"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"RemoveContact");
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
