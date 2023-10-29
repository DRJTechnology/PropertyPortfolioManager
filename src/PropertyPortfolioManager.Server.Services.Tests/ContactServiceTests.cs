using Moq;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Tests.Extensions;

namespace PropertyPortfolioManager.Server.Services.Tests
{
    public class ContactServiceTests
    {
        private List<ContactDto> contactList;
        private List<ContactBasicDto> basicContactList;

        public ContactServiceTests()
        {
            this.contactList = this.PopulateContacts();
            this.basicContactList = this.PopulateBasicContacts();
        }

        [Fact]
        public async void Get_All_Contacts()
        {
            int portfolioId = 2;
            var contactRepositoryMock = new Mock<IContactRepository>(MockBehavior.Strict);
            contactRepositoryMock.Setup(r => r.GetAll(portfolioId, false))
                                        .Returns(Task.FromResult(this.basicContactList));

            var contactService = new ContactService(contactRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var contacts = await contactService.GetAll(portfolioId, false);

            Assert.IsType<List<ContactBasicResponseModel>>(contacts);
            Assert.Equal(10, contacts.Count());
            Assert.Equal(1, contacts.FirstOrDefault().Id);
        }

        [Fact]
        public async void Get_All_Active_Contacts()
        {
            int portfolioId = 2;
            var contactRepositoryMock = new Mock<IContactRepository>(MockBehavior.Strict);
            contactRepositoryMock.Setup(r => r.GetAll(portfolioId, false))
                                        .Returns(Task.FromResult(this.basicContactList.Where(ct => ct.Active).ToList()));

            var contactService = new ContactService(contactRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var contacts = await contactService.GetAll(portfolioId, false);

            Assert.IsType<List<ContactBasicResponseModel>>(contacts);
            Assert.Equal(8, contacts.Count());
            Assert.Equal(1, contacts.FirstOrDefault().Id);
        }


        [Fact]
        public async void Get_Contact_By_Id()
        {
            int portfolioId = 2;
            var contactId = 7;
            var contactRepositoryMock = new Mock<IContactRepository>(MockBehavior.Strict);
            contactRepositoryMock.Setup(r => r.GetById(contactId, portfolioId))
                                        .Returns(Task.FromResult(this.GetById(contactId)));

            var contactService = new ContactService(contactRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var contact = await contactService.GetById(contactId, portfolioId);

            Assert.IsType<ContactResponseModel>(contact);
            Assert.Equal(contactId, contact.Id);
            Assert.Equal($"Name {contactId}", contact.Name);
        }


        [Fact]
        public async void Get_Contact_By_Id_NotFound()
        {
            int portfolioId = 2;
            var contactId = 33;
            var contactRepositoryMock = new Mock<IContactRepository>(MockBehavior.Strict);
            contactRepositoryMock.Setup(r => r.GetById(contactId, portfolioId))
                                        .Returns(Task.FromResult(this.GetById(contactId)));

            var contactService = new ContactService(contactRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var contact = await contactService.GetById(contactId, portfolioId);

            Assert.Null(contact);
        }

        [Fact]
        public async void Create_Contact_No_Address()
        {
            int portfolioId = 2;
            var currentUserId = 2;
            var newContact = new ContactEditModel()
            {
                Name = "Test1",
                ContactTypeId = 1,
            };

            var contactRepositoryMock = new Mock<IContactRepository>(MockBehavior.Strict);
            contactRepositoryMock.Setup(r => r.Create(It.IsAny<int>(), portfolioId, It.IsAny<ContactDto>()))
                                        .Returns(Task.FromResult(57));

            var contactService = new ContactService(contactRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var contactId = await contactService.Create(currentUserId, portfolioId, newContact);

            Assert.Equal(57, contactId);
        }

        [Fact]
        public async void Create_Contact_With_Address()
        {
            int portfolioId = 2;
            var currentUserId = 2;
            var newContact = new ContactEditModel()
            {
                Name = "Test1",
                ContactTypeId = 1,
                Address = new AddressModel()
                {
                    Id = 5,
                    StreetAddress = "7 Short Road",
                    CountyRegion = "StrangeRegion",
                    PostCode = "AB12 34DE",
                }
            };

            var contactRepositoryMock = new Mock<IContactRepository>(MockBehavior.Strict);
            contactRepositoryMock.Setup(r => r.Create(It.IsAny<int>(), portfolioId, It.IsAny<ContactDto>()))
                                        .Returns(Task.FromResult(57));

            var contactService = new ContactService(contactRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var contactId = await contactService.Create(currentUserId, portfolioId, newContact);

            Assert.Equal(57, contactId);
        }

        [Fact]
        public async void Update_Contact_Success()
        {
            int portfolioId = 2;
            var currentUserId = 2;
            var existingContact = new ContactEditModel()
            {
                Id = 2,
                Name = "Test1",
                ContactTypeId = 1,
                Address = new AddressModel()
                {
                    Id = 5,
                    StreetAddress = "7 Short Road",
                    CountyRegion = "StrangeRegion",
                    PostCode = "AB12 34DE",
                }
            };

            var contactRepositoryMock = new Mock<IContactRepository>(MockBehavior.Strict);
            contactRepositoryMock.Setup(r => r.Update(It.IsAny<int>(), portfolioId, It.IsAny<ContactDto>()))
                                        .Returns(Task.FromResult(true));

            var contactService = new ContactService(contactRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var response = await contactService.Update(currentUserId, portfolioId, existingContact);

            Assert.True(response);
        }

        [Fact]
        public async void Update_Contact_Failure()
        {
            int portfolioId = 2;
            var currentUserId = 2;
            var existingContact = new ContactEditModel()
            {
                Id = 2,
                Name = "Test1",
                ContactTypeId = 1,
                Address = new AddressModel()
                {
                    Id = 5,
                    StreetAddress = "7 Short Road",
                    CountyRegion = "StrangeRegion",
                    PostCode = "AB12 34DE",
                }
            };

            var contactRepositoryMock = new Mock<IContactRepository>(MockBehavior.Strict);
            contactRepositoryMock.Setup(r => r.Update(It.IsAny<int>(), portfolioId, It.IsAny<ContactDto>()))
                                        .Returns(Task.FromResult(false));

            var contactService = new ContactService(contactRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var response = await contactService.Update(currentUserId, portfolioId, existingContact);

            Assert.False(response);
        }





        private ContactDto GetById(int contactId)
        {
            return this.contactList.Where(u => u.Id == contactId).FirstOrDefault();
        }

        private List<ContactBasicDto> PopulateBasicContacts()
        {
            var properties = new List<ContactBasicDto>();
            for (int i = 1; i < 11; i++)
            {
                properties.Add(
                    new ContactBasicDto()
                    {
                        Id = i,
                        Name = $"Name {i}",
                        ContactType = "Supplier",
                        StreetAddress = $"{1} Long Road",
                        Active = i != 2 && i != 4,
                    }
                );
            }
            return properties;
        }

        private List<ContactDto> PopulateContacts()
        {
            var properties = new List<ContactDto>();
            for (int i = 1; i < 11; i++)
            {
                properties.Add(
                    new ContactDto()
                    {
                        Id = i,
                        PortfolioId = 2,
                        Name = $"Name {i}",
                        ContactTypeId = 1,
                        ContactType = "Supplier",
                        Active = i != 2 && i != 4,
                        CreateDate = DateTime.Now.AddMonths(-1),
                        CreateUserId = 1,
                        AmendDate = DateTime.Now.AddMonths(-1),
                        AmendUserId = 1,
                    }
                );
            }
            return properties;
        }

    }
}
