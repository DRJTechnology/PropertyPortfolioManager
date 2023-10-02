using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitTypeController : BaseController
    {
        private readonly IUnitTypeService unitTypeService;

        public UnitTypeController(IUserService userService, IUnitTypeService unitTypeService)
            : base(userService)
        {
            this.unitTypeService = unitTypeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<UnitTypeModel>> GetAll()
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if(portfolioId == null)
            {
                return new List<UnitTypeModel>();
            }
            else
            {
                return await this.unitTypeService.GetAll((int)portfolioId);
            }
        }

        [HttpGet]
        [Route("GetById/{unitTypeId}")]
        public async Task<UnitTypeModel> GetById(int unitTypeId)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new UnitTypeModel();
            }
            else
            {
                return await this.unitTypeService.GetById(unitTypeId, (int)portfolioId);
            }         
        }

        [HttpPost]
        [Route("Create")]
        public async Task<PpmApiResponse> Create(UnitTypeModel unitType)
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
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<PpmApiResponse> Update(UnitTypeModel unitType)
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
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

    }
}
