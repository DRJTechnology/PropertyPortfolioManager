﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : BaseController
    {
        private readonly IPortfolioService portfolioService;

        public PortfolioController(IUserService userService, IPortfolioService portfolioService)
            : base(userService)
        {
            this.portfolioService = portfolioService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<PortfolioModel>> GetAll()
        {
            return await this.portfolioService.GetAll((await this.GetCurrentUser()).Id);
        }

        [HttpGet]
        [Route("GetById/{portfolioId}")]
        public async Task<PortfolioModel> GetById(int portfolioId)
        {
            return await this.portfolioService.GetById(portfolioId, (await this.GetCurrentUser()).Id);
        }

        [HttpGet]
        [Route("GetCurrent")]
        public async Task<PortfolioModel> GetCurrent()
        {
            return await this.portfolioService.GetCurrent(User);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ApiCreateResponse> Create(PortfolioModel portfolio)
        {
            try
            {
                var portfolioId = await this.portfolioService.Create((await this.GetCurrentUser()).Id, portfolio);
                return new ApiCreateResponse()
                {
                    CreatedId = portfolioId,
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
        public async Task<ApiCreateResponse> Update(PortfolioModel portfolio, int portfolioId)
        {
            try
            {
                if (await this.portfolioService.Update((await this.GetCurrentUser()).Id, portfolio))
                {
                    return new ApiCreateResponse()
                    {
                        CreatedId = portfolio.Id,
                        Success = true,
                    };
                }
                else
                {
                    return new ApiCreateResponse()
                    {
                        Success = false,
                        ErrorMessage = $"PortfolioController: Failed to update portfolioId {portfolio.Id}"
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
