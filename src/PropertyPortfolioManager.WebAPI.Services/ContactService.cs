using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public ContactService(IContactRepository contactRepository, ICacheService cacheService, IMapper mapper)
        {
            this.contactRepository = contactRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<int> Create(int currentUserId, int portfolioId, ContactEditModel contact)
        {
            var contactDto = this.mapper.Map<ContactDto>(contact);
            return await this.contactRepository.Create(currentUserId, portfolioId, contactDto);
        }

        public async Task<List<ContactBasicResponseModel>> GetAll(int portfolioId, bool activeOnly)
        {
            var contactList = await this.contactRepository.GetAll(portfolioId, activeOnly);
            return this.mapper.Map<List<ContactBasicResponseModel>>(contactList);
        }

        public async Task<ContactResponseModel> GetById(int contactId, int portfolioId)
        {
            var contact = await this.contactRepository.GetById(contactId, portfolioId);
            return this.mapper.Map<ContactResponseModel>(contact);
        }

        public async Task<bool> Update(int currentUserId, int portfolioId, ContactEditModel contact)
        {
            var contactDto = this.mapper.Map<ContactDto>(contact);
            return await this.contactRepository.Update(currentUserId, portfolioId, contactDto);
        }
    }
}
