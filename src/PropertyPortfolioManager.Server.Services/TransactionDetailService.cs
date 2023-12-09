using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Services
{
    public class TransactionDetailService : ITransactionDetailService
    {
        private readonly ITransactionDetailRepository transactionDetailRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public TransactionDetailService(ITransactionDetailRepository transactionDetailRepository, ICacheService cacheService, IMapper mapper)
        {
            this.transactionDetailRepository = transactionDetailRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<List<TransactionDetailResponseModel>> GetAsync(int portfolioId, DateTime? fromDate, DateTime? toDate, int accountId, int transactionTypeId)
        {
            var transactionDetailList = await this.transactionDetailRepository.Get(portfolioId, fromDate, toDate, accountId, transactionTypeId);
            return this.mapper.Map<List<TransactionDetailResponseModel>>(transactionDetailList);
        }
    }
}
