﻿using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Controllers
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
        [Route("GetAll")]
        public async Task<List<UnitBasicResponseModel>> GetAll(int portfolioId)
        {
            return await this.unitService.GetAll(portfolioId);
        }

        [HttpGet]
        [Route("GetById/{unitId}")]
        public async Task<UnitResponseModel> GetById(int unitId, int portfolioId)
        {
            return await this.unitService.GetById(unitId, portfolioId);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ApiCreateResponse> Create(UnitEditModel unit, int portfolioId)
        {
            try
            {
                //var currentUser = await this.UserService.GetCurrent(User);
                //var newUnitId = await this.unitService.Create(currentUser.Id, portfolioId, unit);
                var newUnitId = await this.unitService.Create((await this.GetCurrentUser()).Id, portfolioId, unit);
                return new ApiCreateResponse()
                {
                    CreatedId = newUnitId,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ApiCreateResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ApiCreateResponse> Update(UnitEditModel unit, int portfolioId)
        {
            try
            {
                //var currentUser = await this.UserService.GetCurrent(User);
                if (await this.unitService.Update((await this.GetCurrentUser()).Id, portfolioId, unit))
                {
                    return new ApiCreateResponse()
                    {
                        CreatedId = unit.Id,
                        Success = true,
                    };
                }
                else
                {
                    return new ApiCreateResponse()
                    {
                        Success = false,
                        ErrorMessage = $"UnitController: Failed to update unitId {unit.Id}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiCreateResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

    }
}