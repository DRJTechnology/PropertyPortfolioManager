using Moq;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Tests.Extensions;

namespace PropertyPortfolioManager.WebAPI.Services.Tests
{
    public class ContactTypeServiceTests
    {
        private List<ContactTypeDto> contactTypeList;

        public ContactTypeServiceTests()
        {
            this.contactTypeList = this.PopulateContactTypes();
        }

        [Fact]
        public async void Get_All_ContactTypes()
        {
            var portfolioId = 2;
            var contactTypeRepositoryMock = new Mock<IContactTypeRepository>(MockBehavior.Strict);
            contactTypeRepositoryMock.Setup(r => r.GetAll(portfolioId))
                                        .Returns(Task.FromResult(this.contactTypeList));

            var contactTypeService = new ContactTypeService(contactTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var contacts = await contactTypeService.GetAll(portfolioId);

            Assert.IsType<List<ContactTypeModel>>(contacts);
            Assert.Equal(11, contacts.Count());
            Assert.Equal(1, contacts.FirstOrDefault().Id);
        }

        [Fact]
        public async void Get_ContactType_By_Id()
        {
            var portfolioId = 2;
            var contactTypeId = 4;
            var contactTypeRepositoryMock = new Mock<IContactTypeRepository>(MockBehavior.Strict);
            contactTypeRepositoryMock.Setup(r => r.GetById(contactTypeId, portfolioId))
                                        .Returns(Task.FromResult(this.GetById(contactTypeId)));

            var contactTypeService = new ContactTypeService(contactTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var contact = await contactTypeService.GetById(contactTypeId, portfolioId);

            Assert.IsType<ContactTypeModel>(contact);
            Assert.Equal(contactTypeId, contact.Id);
            Assert.Equal($"Unit Type {contactTypeId}", contact.Type);
        }


        [Fact]
        public async void Get_ContactType_By_Id_NotFound()
        {
            var portfolioId = 2;
            var contactTypeId = 20;
            var contactTypeRepositoryMock = new Mock<IContactTypeRepository>(MockBehavior.Strict);
            contactTypeRepositoryMock.Setup(r => r.GetById(contactTypeId, portfolioId))
                                        .Returns(Task.FromResult(this.GetById(contactTypeId)));

            var contactTypeService = new ContactTypeService(contactTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var contact = await contactTypeService.GetById(contactTypeId, portfolioId);

            Assert.Null(contact);
        }

        [Fact]
        public async void Create_ContactType()
        {
            var portfolioId = 2;
            var currentUserId = 2;
            var newUnit = new ContactTypeModel()
            {
                Type = "TestContactType1",
            };

            var contactTypeRepositoryMock = new Mock<IContactTypeRepository>(MockBehavior.Strict);
            contactTypeRepositoryMock.Setup(r => r.Create(It.IsAny<int>(), portfolioId, It.IsAny<ContactTypeDto>()))
                                        .Returns(Task.FromResult(13));

            var contactTypeService = new ContactTypeService(contactTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var contactTypeId = await contactTypeService.Create(currentUserId, portfolioId, newUnit);

            Assert.Equal(13, contactTypeId);
        }


        private ContactTypeDto GetById(int contactTypeId)
        {
            return this.contactTypeList.Where(u => u.Id == contactTypeId).FirstOrDefault();
        }

        private List<ContactTypeDto> PopulateContactTypes()
        {
            var contactTypes = new List<ContactTypeDto>();
            for (int i = 1; i < 12; i++)
            {
                contactTypes.Add(
                    new ContactTypeDto()
                    {
                        Id = (short)i,
                        PortfolioId = 1 < 5 ? -1 : 2,
                        Type = $"Unit Type {i}",
                        CreateDate = DateTime.Now.AddMonths(-1),
                        CreateUserId = 1,
                        AmendDate = DateTime.Now.AddMonths(-1),
                        AmendUserId = 1,
                    }
                );
            }
            return contactTypes;
        }

    }
}
