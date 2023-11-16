using AutoMapper;
using DRJTechnology.Cache;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Interfaces;
using PropertyPortfolioManager.Server.Shared.Configuration;

namespace PropertyPortfolioManager.Server.Services
{
    public class TenancyService : ITenancyService
    {
        private Settings settings;
        private readonly ITenancyRepository tenancyRepository;
        private readonly ICacheService cacheService;
        private readonly IDocumentService documentService;
        private readonly IMapper mapper;
        private readonly GraphServiceClient graphServiceClient;

        public TenancyService(IOptions<Settings> settings, ITenancyRepository tenancyRepository, ICacheService cacheService, IDocumentService documentService, IMapper mapper, GraphServiceClient graphServiceClient)
        {
            this.settings = settings.Value;
            this.tenancyRepository = tenancyRepository;
            this.cacheService = cacheService;
            this.documentService = documentService;
            this.mapper = mapper;
            this.graphServiceClient = graphServiceClient;
        }

        public async Task<int> Create(int currentUserId, int portfolioId, TenancyEditModel tenancy)
        {
            var tenancyDto = this.mapper.Map<TenancyDto>(tenancy);
            return await this.tenancyRepository.Create(currentUserId, portfolioId, tenancyDto);
        }

        public async Task<bool> Delete(int currentUserId, int portfolioId, int tenancyId)
        {
            return await this.tenancyRepository.Delete(currentUserId, portfolioId, tenancyId);
        }

        public async Task<List<TenancyResponseModel>> GetAll(int portfolioId, bool activeOnly)
        {
            var tenancyList = await this.tenancyRepository.GetAll(portfolioId, activeOnly);
            return this.mapper.Map<List<TenancyResponseModel>>(tenancyList);
        }

        public async Task<TenancyResponseModel> GetById(int tenancyId, int portfolioId)
        {
            try
            {
                var tenancy = await this.tenancyRepository.GetById(tenancyId, portfolioId);
                return this.mapper.Map<TenancyResponseModel>(tenancy);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(int currentUserId, int portfolioId, TenancyEditModel tenancy)
        {
            var tenancyDto = this.mapper.Map<TenancyDto>(tenancy);
            return await this.tenancyRepository.Update(currentUserId, portfolioId, tenancyDto);
        }
    }
}
