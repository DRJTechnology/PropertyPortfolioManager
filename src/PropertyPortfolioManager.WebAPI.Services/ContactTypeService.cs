using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Services
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly IContactTypeRepository ContactTypeRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public ContactTypeService(IContactTypeRepository ContactTypeRepository, ICacheService cacheService, IMapper mapper)
        {
            this.ContactTypeRepository = ContactTypeRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<int> Create(int currentUserId, int portfolioId, ContactTypeModel contact)
        {
            var contactTypeDto = this.mapper.Map<ContactTypeDto>(contact);
            return await this.ContactTypeRepository.Create(currentUserId, portfolioId, contactTypeDto);
        }

        public async Task<List<ContactTypeModel>> GetAll(int portfolioId)
        {
            var contactTypeList = await this.ContactTypeRepository.GetAll(portfolioId);
            return this.mapper.Map<List<ContactTypeModel>>(contactTypeList);
        }

        public async Task<ContactTypeModel> GetById(int ContactId, int portfolioId)
        {
            var contact = await this.ContactTypeRepository.GetById(ContactId, portfolioId);
            return this.mapper.Map<ContactTypeModel>(contact);
        }

        public async Task<bool> Update(int currentUserId, int portfolioId, ContactTypeModel contact)
        {
            var contactTypeDto = this.mapper.Map<ContactTypeDto>(contact);
            return await this.ContactTypeRepository.Update(currentUserId, portfolioId, contactTypeDto);
        }
    }
}
