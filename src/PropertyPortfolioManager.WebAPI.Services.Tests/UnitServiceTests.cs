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

        public UnitServiceTests()
        {
            this.unitList = this.PopulateUnits();
        }

        [Fact]
        public async void Get_All_Units()
        {
            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(Task.FromResult(this.unitList));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var units = await unitService.GetAll();

            Assert.IsType<List<UnitResponseModel>>(units);
            Assert.Equal(10, units.Count());
            Assert.Equal(1, units.FirstOrDefault().Id);
        }


        [Fact]
        public async void Get_Unit_By_Id()
        {
            var unitId = 7;
            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.GetById(unitId))
                                        .Returns(Task.FromResult(this.GetById(unitId)));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unit = await unitService.GetById(unitId);

            Assert.IsType<UnitResponseModel>(unit);
            Assert.Equal(unitId, unit.Id);
            Assert.Equal($"PR7", unit.Code);
        }


        [Fact]
        public async void Get_Unit_By_Id_NotFound()
        {
            var unitId = 33;
            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.GetById(unitId))
                                        .Returns(Task.FromResult(this.GetById(unitId)));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unit = await unitService.GetById(unitId);

            Assert.Null(unit);
        }

        [Fact]
        public async void Create_Unit_No_Address()
        {
            var currentUserId = 2;
            var newUnit = new UnitEditModel()
            {
                Code = "Test1",
                UnitTypeId = 1,
            };

            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.Create(It.IsAny<int>(), It.IsAny<UnitDto>()))
                                        .Returns(Task.FromResult(57));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unitId = await unitService.Create(currentUserId, newUnit);

            Assert.Equal(57, unitId);
        }

        [Fact]
        public async void Create_Unit_With_Address()
        {
            var currentUserId = 2;
            var newUnit = new UnitEditModel()
            {
                Code = "Test1",
                UnitTypeId = 1,
                Address = new AddressEditModel()
                {
                    Id = 5,
                    StreetAddress = "7 Short Road",
                    CountryRegion = "StrangeRegion",
                    PostCode = "AB12 34DE",
                }
            };

            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.Create(It.IsAny<int>(), It.IsAny<UnitDto>()))
                                        .Returns(Task.FromResult(57));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unitId = await unitService.Create(currentUserId, newUnit);

            Assert.Equal(57, unitId);
        }

        [Fact]
        public async void Update_Unit_Success()
        {
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
                    CountryRegion = "StrangeRegion",
                    PostCode = "AB12 34DE",
                }
            };

            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<UnitDto>()))
                                        .Returns(Task.FromResult(true));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var response = await unitService.Update(currentUserId, existingUnit);

            Assert.True(response);
        }

        [Fact]
        public async void Update_Unit_Failure()
        {
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
                    CountryRegion = "StrangeRegion",
                    PostCode = "AB12 34DE",
                }
            };

            var unitRepositoryMock = new Mock<IUnitRepository>(MockBehavior.Strict);
            unitRepositoryMock.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<UnitDto>()))
                                        .Returns(Task.FromResult(false));

            var unitService = new UnitService(unitRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var response = await unitService.Update(currentUserId, existingUnit);

            Assert.False(response);
        }





        private UnitDto GetById(int unitId)
        {
            return this.unitList.Where(u => u.Id == unitId).FirstOrDefault();
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
                        Code = $"PR{i}",
                        UnitTypeId = 1,
                        UnitType = new UnitTypeDto()
                        {
                            Id = 1,
                            Type = "Detached",
                            CreateDate = DateTime.Now.AddMonths(-1),
                            CreateUserId = 1,
                            AmendDate = DateTime.Now.AddMonths(-1),
                            AmendUserId = 1,
                        },
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
