using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Services
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IAccountTypeRepository accountTypeRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public AccountTypeService(IAccountTypeRepository accountTypeRepository, ICacheService cacheService, IMapper mapper)
        {
            this.accountTypeRepository = accountTypeRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<List<AccountTypeResponseModel>> GetAll()
        {
            var accountList = await this.accountTypeRepository.GetAll();
            return this.mapper.Map<List<AccountTypeResponseModel>>(accountList);
        }
    }
}
