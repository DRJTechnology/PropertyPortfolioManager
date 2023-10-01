﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<List<UnitTypeModel>> GetAll(int portfolioId)
        {
            return await this.unitTypeService.GetAll(portfolioId);
        }

        [HttpGet]
        [Route("GetById/{unitTypeId}")]
        public async Task<UnitTypeModel> GetById(int unitTypeId, int portfolioId)
        {
            return await this.unitTypeService.GetById(unitTypeId, portfolioId);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ApiCreateResponse> Create(UnitTypeModel unitType, int portfolioId)
        {
            try
            {
                //var currentUser = await this.UserService.GetCurrent(User);
                var newUnitTypeId = await this.unitTypeService.Create((await this.GetCurrentUser()).Id, portfolioId, unitType);
                return new ApiCreateResponse()
                {
                    CreatedId = newUnitTypeId,
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
        public async Task<ApiCreateResponse> Update(UnitTypeModel unitType, int portfolioId)
        {
            try
            {
                //var currentUser = await this.UserService.GetCurrent(User);
                if (await this.unitTypeService.Update((await this.GetCurrentUser()).Id, portfolioId, unitType))
                {
                    return new ApiCreateResponse()
                    {
                        CreatedId = unitType.Id,
                        Success = true,
                    };
                }
                else
                {
                    return new ApiCreateResponse()
                    {
                        Success = false,
                        ErrorMessage = $"UnitTypeController: Failed to update unitTypeId {unitType.Id}"
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