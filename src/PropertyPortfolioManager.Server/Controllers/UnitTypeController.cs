using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitTypeController : BaseController
    {
        private readonly ILogger<PortfolioController> logger;
        private readonly IUnitTypeService unitTypeService;

        public UnitTypeController(ILogger<PortfolioController> logger, IUserService userService, IUnitTypeService unitTypeService)
            : base(userService)
        {
            this.logger = logger;
            this.unitTypeService = unitTypeService;
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
                    var units = await this.unitTypeService.GetAll((int)portfolioId, activeOnly);
                    return this.Ok(units);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetAll");
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetById/{unitTypeId}")]
        public async Task<IActionResult> GetById(int unitTypeId)
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
                    var unitType = await this.unitTypeService.GetById(unitTypeId, (int)portfolioId);
                    return this.Ok(unitType);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetById{unitTypeId}");
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<PpmApiResponse> Create(EntityTypeModel unitType)
        {
            try
            {
                var newUnitTypeId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "UnitType_Create: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    newUnitTypeId = await this.unitTypeService.Create((await this.GetCurrentUser()).Id, (int)portfolioId, unitType);
                }

                return new PpmApiResponse()
                {
                    CreatedId = newUnitTypeId,
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
        public async Task<PpmApiResponse> Update(EntityTypeModel unitType)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "UnitType_Update: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.unitTypeService.Update((await this.GetCurrentUser()).Id, (int)portfolioId, unitType))
                    {
                        return new PpmApiResponse()
                        {
                            CreatedId = unitType.Id,
                            Success = true,
                        };
                    }
                    else
                    {
                        return new PpmApiResponse()
                        {
                            Success = false,
                            ErrorMessage = $"UnitTypeController: Failed to update unitTypeId {unitType.Id}"
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
        [Route("Delete/{unitTypeId}")]
        public async Task<PpmApiResponse> Delete(int unitTypeId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "UnitType_Delete: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.unitTypeService.Delete((await this.GetCurrentUser()).Id, (int)portfolioId, unitTypeId))
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
                            ErrorMessage = $"UnitTypeController: Failed to delete unitTypeId {unitTypeId}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete/{unitTypeId}");
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
