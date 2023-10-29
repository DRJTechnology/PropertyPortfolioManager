using AutoMapper;
using DRJTechnology.Cache;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Services
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly IContactTypeRepository contactTypeRepository;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public ContactTypeService(IContactTypeRepository contactTypeRepository, ICacheService cacheService, IMapper mapper)
        {
            this.contactTypeRepository = contactTypeRepository;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<int> Create(int currentUserId, int portfolioId, ContactTypeModel contact)
        {
            var contactTypeDto = this.mapper.Map<ContactTypeDto>(contact);
            return await this.contactTypeRepository.Create(currentUserId, portfolioId, contactTypeDto);
        }

        public async Task<bool> Delete(int currentUserId, int portfolioId, int contactTypeId)
        {
            return await this.contactTypeRepository.Delete(currentUserId, portfolioId, contactTypeId);
        }

        public async Task<List<ContactTypeModel>> GetAll(int portfolioId, bool activeOnly)
        {
            var contactTypeList = await this.contactTypeRepository.GetAll(portfolioId, activeOnly);
            return this.mapper.Map<List<ContactTypeModel>>(contactTypeList);
        }

        public async Task<ContactTypeModel> GetById(int ContactId, int portfolioId)
        {
            var contact = await this.contactTypeRepository.GetById(ContactId, portfolioId);
            return this.mapper.Map<ContactTypeModel>(contact);
        }

        public async Task<bool> Update(int currentUserId, int portfolioId, ContactTypeModel contact)
        {
            var contactTypeDto = this.mapper.Map<ContactTypeDto>(contact);
            return await this.contactTypeRepository.Update(currentUserId, portfolioId, contactTypeDto);
        }
    }
}
