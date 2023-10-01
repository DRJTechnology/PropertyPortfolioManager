using AutoMapper;
using DRJTechnology.Cache;
using Microsoft.Identity.Web;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;
using System.Security.Claims;

namespace PropertyPortfolioManager.WebAPI.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository portfolioRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public PortfolioService(IPortfolioRepository portfolioRepository, ICacheService cacheService, IMapper mapper)
        {
            this.portfolioRepository = portfolioRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<int> Create(int currentUserId, PortfolioModel portfolio)
        {
            var portfolioDto = this.mapper.Map<PortfolioDto>(portfolio);
            return await this.portfolioRepository.Create(currentUserId, portfolioDto);
        }

        public async Task<List<PortfolioModel>> GetAll(int userId)
        {
            var portfolioList = await this.portfolioRepository.GetAll(userId);
            return this.mapper.Map<List<PortfolioModel>>(portfolioList);
        }

        public async Task<PortfolioModel> GetById(int portfolioId, int userId)
        {
            var portfolio = await this.portfolioRepository.GetById(portfolioId, userId);
            return this.mapper.Map<PortfolioModel>(portfolio);
        }

        public async Task<PortfolioModel> GetCurrent(ClaimsPrincipal user)
        {
            var cacheKey = $"CurrentPortfolio_{user.GetObjectId()}";
            var currentPortfolio = await this.cacheService.GetAsync<PortfolioModel>(cacheKey);

            if (currentPortfolio != null)
            {
                return currentPortfolio;
            }

            var userObjectIdentifier = new Guid(user.GetObjectId()!);
            var portfolioDto = await this.portfolioRepository.GetByUserObjectIdentifier(userObjectIdentifier);

            var portfolio = this.mapper.Map<PortfolioModel>(portfolioDto);
            if (portfolio != null)
            {
                await this.cacheService.SetAsync(cacheKey, portfolio);
            }
            else
            {
                portfolio = new PortfolioModel();
            }

            return portfolio;
        }

        public async Task<bool> Update(int currentUserId, PortfolioModel portfolio)
        {
            var portfolioDto = this.mapper.Map<PortfolioDto>(portfolio);
            return await this.portfolioRepository.Update(currentUserId, portfolioDto);
        }
    }
}
