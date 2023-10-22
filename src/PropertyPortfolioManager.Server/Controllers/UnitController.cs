using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : BaseController
    {
        private readonly IUnitService unitService;

        public UnitController(IUserService userService, IUnitService unitService)
            : base(userService)
        {
            this.unitService = unitService;
        }

        [HttpGet]
        [Route("GetAll/{activeOnly}")]
        public async Task<List<UnitBasicResponseModel>> GetAll(bool activeOnly)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new List<UnitBasicResponseModel>();
            }
            else
            {
                return await this.unitService.GetAll((int)portfolioId, activeOnly);
            }
        }

        [HttpGet]
        [Route("GetById/{unitId}")]
        public async Task<UnitResponseModel> GetById(int unitId)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new UnitResponseModel();
            }
            else
            {
                return await this.unitService.GetById(unitId, (int)portfolioId);
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
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

    }
}
