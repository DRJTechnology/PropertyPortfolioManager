using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
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
		[Route("GetAll/{activeOnly}")]
		public async Task<List<PortfolioModel>> GetAll(bool activeOnly)
		{
			try
			{
				var returnVal = await this.portfolioService.GetAll((await this.GetCurrentUser()).Id, activeOnly);
				return returnVal;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		[HttpGet]
		[Route("GetById/{portfolioId}")]
		public async Task<PortfolioModel> GetById(int portfolioId)
		{
			var portfolio = await this.portfolioService.GetById(portfolioId, (await this.GetCurrentUser()).Id);
			return portfolio;
        }

		[HttpGet]
		[Route("GetCurrent")]
		public async Task<PortfolioModel> GetCurrent()
		{
			return await this.portfolioService.GetCurrent(User);
		}

		[HttpPost]
		[Route("Create")]
		public async Task<PpmApiResponse> Create(PortfolioModel portfolio)
		{
			try
			{
				var portfolioId = await this.portfolioService.Create((await this.GetCurrentUser()).Id, portfolio);
				return new PpmApiResponse()
				{
					CreatedId = portfolioId,
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
		public async Task<PpmApiResponse> Update(PortfolioModel portfolio, int portfolioId)
		{
			try
			{
				if (await this.portfolioService.Update((await this.GetCurrentUser()).Id, portfolio))
				{
					return new PpmApiResponse()
					{
						CreatedId = portfolio.Id,
						Success = true,
					};
				}
				else
				{
					return new PpmApiResponse()
					{
						Success = false,
						ErrorMessage = $"PortfolioController: Failed to update portfolioId {portfolio.Id}"
					};
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

		[HttpGet]
		[Route("SelectForCurrentUser/{portfolioId}")]
		public async Task<PpmApiResponse> SelectForCurrentUser(int portfolioId)
		{
			if (await this.portfolioService.SelectForUser(portfolioId, (await this.GetCurrentUser()).Id, User))
			{
				return new PpmApiResponse()
				{
					Success = true
				};
			}
			else
			{
				return new PpmApiResponse()
				{
					Success = false,
					ErrorMessage = "Failed to set current portfolio for user",
				};
			}

		}

        [HttpDelete]
        [Route("Delete/{portfolioId}")]
        public async Task<PpmApiResponse> Delete(int portfolioId)
        {
            try
            {
                if (await this.portfolioService.Delete((await this.GetCurrentUser()).Id, portfolioId))
                {
                    return new PpmApiResponse()
                    {
                        CreatedId = 0,
                        Success = true,
                    };
                }
                else
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = $"PortfolioController: Failed to delete portfolioId {portfolioId}"
                    };
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
