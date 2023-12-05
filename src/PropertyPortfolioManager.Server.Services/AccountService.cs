using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Dto.Finance;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public AccountService(IAccountRepository accountRepository, ICacheService cacheService, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<int> Create(int currentUserId, int portfolioId, AccountEditModel account)
        {
            var accountDto = this.mapper.Map<AccountDto>(account);
            return await this.accountRepository.Create(currentUserId, portfolioId, accountDto);
        }

        public async Task<List<AccountResponseModel>> GetAll(int portfolioId, bool activeOnly)
        {
            var accountList = await this.accountRepository.GetAll(portfolioId, activeOnly);
            return this.mapper.Map<List<AccountResponseModel>>(accountList);
        }

        public async Task<AccountResponseModel> GetById(int accountId, int portfolioId)
        {
            var account = await this.accountRepository.GetById(accountId, portfolioId);
            return this.mapper.Map<AccountResponseModel>(account);
        }

        public async Task<bool> Update(int currentUserId, int portfolioId, AccountEditModel account)
        {
            var accountDto = this.mapper.Map<AccountDto>(account);
            return await this.accountRepository.Update(currentUserId, portfolioId, accountDto);
        }

        public async Task<bool> Delete(int currentUserId, int portfolioId, int accountId)
        {
            return await this.accountRepository.Delete(currentUserId, portfolioId, accountId);
        }
    }
}
