using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly ITransactionTypeRepository transactionTypeRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public TransactionTypeService(ITransactionTypeRepository transactionTypeRepository, ICacheService cacheService, IMapper mapper)
        {
            this.transactionTypeRepository = transactionTypeRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<List<EntityTypeBasicModel>> GetAll()
        {
            var transactionTypeList = await this.transactionTypeRepository.GetAll();
            return this.mapper.Map<List<EntityTypeBasicModel>>(transactionTypeList);
        }
    }
}
