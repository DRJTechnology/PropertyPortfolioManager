using AutoMapper;
using DRJTechnology.Cache;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using PropertyPortfolioManager.Models.CacheKeys;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Interfaces;
using System.Security.Claims;

namespace PropertyPortfolioManager.Server.Services
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

        public async Task<bool> DeleteById(int portfolioId, int currentUserId)
        {
            var portfolio = await this.portfolioRepository.GetById(portfolioId, currentUserId);
            portfolio.Deleted = true;
            var portfolioDto = this.mapper.Map<PortfolioDto>(portfolio);
            return await this.portfolioRepository.Update(currentUserId, portfolioDto);
        }

        public async Task<List<PortfolioModel>> GetAll(int userId, bool activeOnly)
        {
            //try
            //{
                var portfolioList = await this.portfolioRepository.GetAll(userId, activeOnly);
                return this.mapper.Map<List<PortfolioModel>>(portfolioList);
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        public async Task<PortfolioModel> GetById(int portfolioId, int userId)
        {
            var portfolio = await this.portfolioRepository.GetById(portfolioId, userId);
            return this.mapper.Map<PortfolioModel>(portfolio);
        }

        public async Task<PortfolioModel> GetCurrent(ClaimsPrincipal user)
        {
            var cacheKey = $"{CacheKeys.KeyPortfolioPrefix}{user.GetObjectId()}";
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

        public async Task<bool> SelectForUser(int portfolioId, int userId, ClaimsPrincipal user)
        {
            await this.cacheService.RemoveAsync($"{CacheKeys.KeyPortfolioPrefix}{user.GetObjectId()}");
            await this.cacheService.RemoveAsync($"{CacheKeys.KeyUserPrefix}{user.GetObjectId()}");

            return await this.portfolioRepository.SelectForUser(portfolioId, userId);
        }

        public async Task<bool> Update(int currentUserId, PortfolioModel portfolio)
        {
            var portfolioDto = this.mapper.Map<PortfolioDto>(portfolio);
            return await this.portfolioRepository.Update(currentUserId, portfolioDto);
        }

        public async Task<bool> Delete(int currentUserId, int portfolioId)
        {
            return await this.portfolioRepository.Delete(currentUserId, portfolioId);
        }

    }
}
