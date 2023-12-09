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
        private readonly IUnitTypeService unitTypeService;

        public UnitTypeController(IUserService userService, IUnitTypeService unitTypeService)
            : base(userService)
        {
            this.unitTypeService = unitTypeService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<List<EntityTypeModel>> GetAll()
        {
            return await this.GetAll(true);
        }

        [HttpGet]
        [Route("GetAll/{activeOnly}")]
        public async Task<List<EntityTypeModel>> GetAll(bool activeOnly)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new List<EntityTypeModel>();
            }
            else
            {
                return await this.unitTypeService.GetAll((int)portfolioId, activeOnly);
            }
        }

        [HttpGet]
        [Route("GetById/{unitTypeId}")]
        public async Task<EntityTypeModel> GetById(int unitTypeId)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new EntityTypeModel();
            }
            else
            {
                return await this.unitTypeService.GetById(unitTypeId, (int)portfolioId);
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
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
