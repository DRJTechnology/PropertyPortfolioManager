using AutoMapper;
using Moq;
using PropertyPortfolioManager.Models.Automapper;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Tests.Extensions;

namespace PropertyPortfolioManager.WebAPI.Services.Tests
{
    public class UnitServiceTests
    {
        private List<UnitDto> unitList;
        private List<UnitBasicDto> basicUnitList;

        public UnitServiceTests()
        {
            this.unitList = this.PopulateUnits();
            this.basicUnitList = this.PopulateBasicUnits();
        }

        [Fact]
        public async void Get_All_Units()
        {
            int portfolioId = 2;
            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.GetAll(portfolioId, false))
                                        .Returns(Task.FromResult(this.basicUnitList));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var units = await unitService.GetAll(portfolioId, false);

            Assert.IsType<List<UnitBasicResponseModel>>(units);
            Assert.Equal(10, units.Count());
            Assert.Equal(1, units.FirstOrDefault().Id);
        }

        [Fact]
        public async void Get_All_Active_Units()
        {
            int portfolioId = 2;
            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.GetAll(portfolioId, false))
                                        .Returns(Task.FromResult(this.basicUnitList.Where(ct => ct.Active).ToList()));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var units = await unitService.GetAll(portfolioId, false);

            Assert.IsType<List<UnitBasicResponseModel>>(units);
            Assert.Equal(8, units.Count());
            Assert.Equal(1, units.FirstOrDefault().Id);
        }


        [Fact]
        public async void Get_Unit_By_Id()
        {
            int portfolioId = 2;
            var unitId = 7;
            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.GetById(unitId, portfolioId))
                                        .Returns(Task.FromResult(this.GetById(unitId)));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unit = await unitService.GetById(unitId, portfolioId);

            Assert.IsType<UnitResponseModel>(unit);
            Assert.Equal(unitId, unit.Id);
            Assert.Equal($"PR{unitId}", unit.Code);
        }


        [Fact]
        public async void Get_Unit_By_Id_NotFound()
        {
            int portfolioId = 2;
            var unitId = 33;
            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.GetById(unitId, portfolioId))
                                        .Returns(Task.FromResult(this.GetById(unitId)));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unit = await unitService.GetById(unitId, portfolioId);

            Assert.Null(unit);
        }

        [Fact]
        public async void Create_Unit_No_Address()
        {
            int portfolioId = 2;
            var currentUserId = 2;
            var newUnit = new UnitEditModel()
            {
                Code = "Test1",
                UnitTypeId = 1,
            };

            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.Create(It.IsAny<int>(), portfolioId, It.IsAny<UnitDto>()))
                                        .Returns(Task.FromResult(57));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unitId = await unitService.Create(currentUserId, portfolioId, newUnit);

            Assert.Equal(57, unitId);
        }

        [Fact]
        public async void Create_Unit_With_Address()
        {
            int portfolioId = 2;
            var currentUserId = 2;
            var newUnit = new UnitEditModel()
            {
                Code = "Test1",
                UnitTypeId = 1,
                Address = new AddressEditModel()
                {
                    Id = 5,
                    StreetAddress = "7 Short Road",
                    CountyRegion = "StrangeRegion",
                    PostCode = "AB12 34DE",
                }
            };

            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.Create(It.IsAny<int>(), portfolioId, It.IsAny<UnitDto>()))
                                        .Returns(Task.FromResult(57));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unitId = await unitService.Create(currentUserId, portfolioId, newUnit);

            Assert.Equal(57, unitId);
        }

        [Fact]
        public async void Update_Unit_Success()
        {
            int portfolioId = 2;
            var currentUserId = 2;
            var existingUnit = new UnitEditModel()
            {
                Id = 2,
                Code = "Test1",
                UnitTypeId = 1,
                Address = new AddressEditModel()
                {
                    Id = 5,
                    StreetAddress = "7 Short Road",
                    CountyRegion = "StrangeRegion",
                    PostCode = "AB12 34DE",
                }
            };

            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.Update(It.IsAny<int>(), portfolioId, It.IsAny<UnitDto>()))
                                        .Returns(Task.FromResult(true));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var response = await unitService.Update(currentUserId, portfolioId, existingUnit);

            Assert.True(response);
        }

        [Fact]
        public async void Update_Unit_Failure()
        {
            int portfolioId = 2;
            var currentUserId = 2;
            var existingUnit = new UnitEditModel()
            {
                Id = 2,
                Code = "Test1",
                UnitTypeId = 1,
                Address = new AddressEditModel()
                {
                    Id = 5,
                    StreetAddress = "7 Short Road",
                    CountyRegion = "StrangeRegion",
                    PostCode = "AB12 34DE",
                }
            };

            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.Update(It.IsAny<int>(), portfolioId, It.IsAny<UnitDto>()))
                                        .Returns(Task.FromResult(false));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var response = await unitService.Update(currentUserId, portfolioId, existingUnit);

            Assert.False(response);
        }





        private UnitDto GetById(int unitId)
        {
            return this.unitList.Where(u => u.Id == unitId).FirstOrDefault();
        }

        private List<UnitBasicDto> PopulateBasicUnits()
        {
            var properties = new List<UnitBasicDto>();
            for (int i = 1; i < 11; i++)
            {
                properties.Add(
                    new UnitBasicDto()
                    {
                        Id = i,
                        Code = $"PR{i}",
                        UnitType = "Detached House",
                        StreetAddress = $"{1} Long Road",
                        Active = i != 2 && i != 4,
                    }
                );
            }
            return properties;
        }

        private List<UnitDto> PopulateUnits()
        {
            var properties = new List<UnitDto>();
            for (int i = 1; i < 11; i++)
            {
                properties.Add(
                    new UnitDto()
                    {
                        Id = i,
                        PortfolioId = 2,
                        Code = $"PR{i}",
                        UnitTypeId = 1,
                        UnitType = "Detached House",
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
