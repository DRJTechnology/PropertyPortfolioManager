using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenancyTypeController : BaseController
    {
        private readonly ILogger<TenancyTypeController> logger;
        private readonly ITenancyTypeService tenancyTypeService;

        public TenancyTypeController(ILogger<TenancyTypeController> logger, IUserService userService, ITenancyTypeService tenancyTypeService)
            : base(userService)
        {
            this.logger = logger;
            this.tenancyTypeService = tenancyTypeService;
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
                    return this.Ok(new List<EntityTypeModel>());
                }
                else
                {
                    var returnVal = await this.tenancyTypeService.GetAll((int)portfolioId, activeOnly);
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
        [Route("GetById/{tenancyTypeId}")]
        public async Task<IActionResult> GetById(int tenancyTypeId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return this.Ok(new EntityTypeModel());
                }
                else
                {
                    var tenancyType = await this.tenancyTypeService.GetById(tenancyTypeId, (int)portfolioId);
                    return this.Ok(tenancyType);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetById{tenancyTypeId}");
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<PpmApiResponse> Create(EntityTypeModel tenancyType)
        {
            try
            {
                var newTenancyTypeId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "TenancyType_Create: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    newTenancyTypeId = await this.tenancyTypeService.Create((await this.GetCurrentUser()).Id, (int)portfolioId, tenancyType);
                }

                return new PpmApiResponse()
                {
                    CreatedId = newTenancyTypeId,
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
        public async Task<PpmApiResponse> Update(EntityTypeModel tenancyType)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "TenancyType_Update: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.tenancyTypeService.Update((await this.GetCurrentUser()).Id, (int)portfolioId, tenancyType))
                    {
                        return new PpmApiResponse()
                        {
                            CreatedId = tenancyType.Id,
                            Success = true,
                        };
                    }
                    else
                    {
                        return new PpmApiResponse()
                        {
                            Success = false,
                            ErrorMessage = $"TenancyTypeController: Failed to update tenancyTypeId {tenancyType.Id}"
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
        [Route("Delete/{tenancyTypeId}")]
        public async Task<PpmApiResponse> Delete(int tenancyTypeId)
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
                    if (await this.tenancyTypeService.Delete((await this.GetCurrentUser()).Id, (int)portfolioId, tenancyTypeId))
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
                            ErrorMessage = $"TenancyTypeController: Failed to delete tenancyTypeId {tenancyTypeId}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete/{tenancyTypeId}");
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
