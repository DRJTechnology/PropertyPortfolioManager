using Moq;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Tests.Extensions;

namespace PropertyPortfolioManager.WebAPI.Services.Tests
{
    public class PortfolioServiceTests
    {
        private List<PortfolioDto> portfolioList;

        public PortfolioServiceTests()
        {
            this.portfolioList = this.PopulatePortfolios();
        }

        [Fact]
        public async void Get_All_Portfolios()
        {
            var userId = 99;
            var portfolioRepositoryMock = new Mock<IPortfolioRepository>(MockBehavior.Strict);
            portfolioRepositoryMock.Setup(r => r.GetAll(userId, false))
                                        .Returns(Task.FromResult(this.portfolioList));

            var portfolioService = new PortfolioService(portfolioRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var portfolios = await portfolioService.GetAll(userId, false);

            Assert.IsType<List<PortfolioModel>>(portfolios);
            Assert.Equal(7, portfolios.Count());
            Assert.Equal(1, portfolios.FirstOrDefault().Id);
        }

        [Fact]
        public async void Get_All_Active_Portfolios()
        {
            var userId = 99;
            var portfolioRepositoryMock = new Mock<IPortfolioRepository>(MockBehavior.Strict);
            portfolioRepositoryMock.Setup(r => r.GetAll(userId, false))
                                        .Returns(Task.FromResult(this.portfolioList.Where(ct => ct.Active).ToList()));

            var portfolioService = new PortfolioService(portfolioRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var portfolios = await portfolioService.GetAll(userId, false);

            Assert.IsType<List<PortfolioModel>>(portfolios);
            Assert.Equal(5, portfolios.Count());
            Assert.Equal(1, portfolios.FirstOrDefault().Id);
        }

        [Fact]
        public async void Get_Portfolio_By_Id()
        {
            var portfolioId = 2;
            var userId = 99;
            var portfolioRepositoryMock = new Mock<IPortfolioRepository>(MockBehavior.Strict);
            portfolioRepositoryMock.Setup(r => r.GetById(portfolioId, userId))
                                        .Returns(Task.FromResult(this.GetById(portfolioId)));

            var portfolioService = new PortfolioService(portfolioRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var portfolio = await portfolioService.GetById(portfolioId, userId);

            Assert.IsType<PortfolioModel>(portfolio);
            Assert.Equal(portfolioId, portfolio.Id);
            Assert.Equal($"Portfolio name {portfolioId}", portfolio.Name);
        }

        [Fact]
        public async void Get_Portfolio_By_Id_NotFound()
        {
            var portfolioId = 27;
            var userId = 99;
            var portfolioRepositoryMock = new Mock<IPortfolioRepository>(MockBehavior.Strict);
            portfolioRepositoryMock.Setup(r => r.GetById(portfolioId, userId))
                                        .Returns(Task.FromResult(this.GetById(portfolioId)));

            var portfolioService = new PortfolioService(portfolioRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var portfolio = await portfolioService.GetById(portfolioId, userId);

            Assert.Null(portfolio);
        }

        [Fact]
        public async void Create_Portfolio()
        {
            var currentUserId = 2;
            var newPortfolio = new PortfolioModel()
            {
                Name = "TestPortfolio1",
            };

            var portfolioRepositoryMock = new Mock<IPortfolioRepository>(MockBehavior.Strict);
            portfolioRepositoryMock.Setup(r => r.Create(It.IsAny<int>(), It.IsAny<PortfolioDto>()))
                                        .Returns(Task.FromResult(11));

            var portfolioService = new PortfolioService(portfolioRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var portfolioId = await portfolioService.Create(currentUserId, newPortfolio);

            Assert.Equal(11, portfolioId);
        }


        private PortfolioDto GetById(int portfolioId)
        {
            return this.portfolioList.Where(u => u.Id == portfolioId).FirstOrDefault();
        }

        private List<PortfolioDto> PopulatePortfolios()
        {
            var portfolios = new List<PortfolioDto>();
            for (int i = 1; i < 8; i++)
            {
                portfolios.Add(
                    new PortfolioDto()
                    {
                        Id = (short)i,
                        Name = $"Portfolio name {i}",
                        Active = i != 2 && i != 4,
                        CreateDate = DateTime.Now.AddMonths(-1),
                        CreateUserId = 1,
                        AmendDate = DateTime.Now.AddMonths(-1),
                        AmendUserId = 1,
                    }
                );
            }
            return portfolios;
        }
    }
}
