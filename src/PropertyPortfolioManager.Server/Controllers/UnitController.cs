using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : BaseController
    {
        private readonly ILogger<UnitController> logger;
        private readonly IUnitService unitService;

        public UnitController(ILogger<UnitController> logger, IUserService userService, IUnitService unitService)
            : base(userService)
        {
            this.logger = logger;
            this.unitService = unitService;
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
                    return this.Ok(new List<UnitBasicResponseModel>());
                }
                else
                {
                    var returnVal = await this.unitService.GetAll((int)portfolioId, activeOnly);
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
        [Route("GetById/{unitId}")]
        public async Task<IActionResult> GetById(int unitId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return this.Ok(new UnitResponseModel());
                }
                else
                {
                    var unit = await this.unitService.GetById(unitId, (int)portfolioId);
                    return this.Ok(unit);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetById{unitId}");
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<PpmApiResponse> Create(UnitEditModel unit)
        {
            try
            {
                var newUnitId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Unit_Create: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    newUnitId = await this.unitService.Create((await this.GetCurrentUser()).Id, (int)portfolioId, unit);
                }
                return new PpmApiResponse()
                {
                    CreatedId = newUnitId,
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
        public async Task<PpmApiResponse> Update(UnitEditModel unit)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Unit_Update: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.unitService.Update((await this.GetCurrentUser()).Id, (int)portfolioId, unit))
                    {
                        return new PpmApiResponse()
                        {
                            CreatedId = unit.Id,
                            Success = true,
                        };
                    }
                    else
                    {
                        return new PpmApiResponse()
                        {
                            Success = false,
                            ErrorMessage = $"UnitController: Failed to update unitId {unit.Id}"
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
        [Route("Delete/{unitId}")]
        public async Task<PpmApiResponse> Delete(int unitId)
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
                    if (await this.unitService.Delete((await this.GetCurrentUser()).Id, (int)portfolioId, unitId))
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
                            ErrorMessage = $"UnitTypeController: Failed to delete unitTypeId {unitId}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete/{unitId}");
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
