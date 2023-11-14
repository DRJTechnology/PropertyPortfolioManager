using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenancyTypeController : BaseController
    {
        private readonly ITenancyTypeService tenancyTypeService;

        public TenancyTypeController(IUserService userService, ITenancyTypeService tenancyTypeService)
            : base(userService)
        {
            this.tenancyTypeService = tenancyTypeService;
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
                return await this.tenancyTypeService.GetAll((int)portfolioId, activeOnly);
            }
        }

        [HttpGet]
        [Route("GetById/{tenancyTypeId}")]
        public async Task<EntityTypeModel> GetById(int tenancyTypeId)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new EntityTypeModel();
            }
            else
            {
                return await this.tenancyTypeService.GetById(tenancyTypeId, (int)portfolioId);
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
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
