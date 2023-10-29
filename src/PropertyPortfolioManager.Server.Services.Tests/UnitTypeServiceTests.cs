using Moq;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyPortfolioManager.Server.Services.Tests
{
    public class UnitTypeServiceTests
    {
        private List<UnitTypeDto> unitTypeList;

        public UnitTypeServiceTests()
        {
            this.unitTypeList = this.PopulateUnitTypes();
        }

        [Fact]
        public async void Get_All_UnitTypes()
        {
            var portfolioId = 2;
            var unitTypeRepositoryMock = new Mock<IUnitTypeRepository>(MockBehavior.Strict);
            unitTypeRepositoryMock.Setup(r => r.GetAll(portfolioId, false))
                                        .Returns(Task.FromResult(this.unitTypeList));

            var unitTypeService = new UnitTypeService(unitTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var units = await unitTypeService.GetAll(portfolioId, false);

            Assert.IsType<List<UnitTypeModel>>(units);
            Assert.Equal(7, units.Count());
            Assert.Equal(1, units.FirstOrDefault().Id);
        }

        [Fact]
        public async void Get_All_Active_UnitTypes()
        {
            var portfolioId = 2;
            var unitTypeRepositoryMock = new Mock<IUnitTypeRepository>(MockBehavior.Strict);
            unitTypeRepositoryMock.Setup(r => r.GetAll(portfolioId, false))
                                        .Returns(Task.FromResult(this.unitTypeList.Where(ct => ct.Active).ToList()));

            var unitTypeService = new UnitTypeService(unitTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var units = await unitTypeService.GetAll(portfolioId, false);

            Assert.IsType<List<UnitTypeModel>>(units);
            Assert.Equal(5, units.Count());
            Assert.Equal(1, units.FirstOrDefault().Id);
        }

        [Fact]
        public async void Get_UnitType_By_Id()
        {
            var portfolioId = 2;
            var unitTypeId = 4;
            var unitTypeRepositoryMock = new Mock<IUnitTypeRepository>(MockBehavior.Strict);
            unitTypeRepositoryMock.Setup(r => r.GetById(unitTypeId, portfolioId))
                                        .Returns(Task.FromResult(this.GetById(unitTypeId)));

            var unitTypeService = new UnitTypeService(unitTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unit = await unitTypeService.GetById(unitTypeId, portfolioId);

            Assert.IsType<UnitTypeModel>(unit);
            Assert.Equal(unitTypeId, unit.Id);
            Assert.Equal($"Unit Type {unitTypeId}", unit.Type);
        }


        [Fact]
        public async void Get_UnitType_By_Id_NotFound()
        {
            var portfolioId = 2;
            var unitTypeId = 33;
            var unitTypeRepositoryMock = new Mock<IUnitTypeRepository>(MockBehavior.Strict);
            unitTypeRepositoryMock.Setup(r => r.GetById(unitTypeId, portfolioId))
                                        .Returns(Task.FromResult(this.GetById(unitTypeId)));

            var unitTypeService = new UnitTypeService(unitTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unit = await unitTypeService.GetById(unitTypeId, portfolioId);

            Assert.Null(unit);
        }

        [Fact]
        public async void Create_UnitType()
        {
            var portfolioId = 2;
            var currentUserId = 2;
            var newUnit = new UnitTypeModel()
            {
                Type = "TestUnitType1",
            };

            var unitTypeRepositoryMock = new Mock<IUnitTypeRepository>(MockBehavior.Strict);
            unitTypeRepositoryMock.Setup(r => r.Create(It.IsAny<int>(), portfolioId, It.IsAny<UnitTypeDto>()))
                                        .Returns(Task.FromResult(11));

            var unitTypeService = new UnitTypeService(unitTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unitTypeId = await unitTypeService.Create(currentUserId, portfolioId, newUnit);

            Assert.Equal(11, unitTypeId);
        }


        private UnitTypeDto GetById(int unitTypeId)
        {
            return this.unitTypeList.Where(u => u.Id == unitTypeId).FirstOrDefault();
        }

        private List<UnitTypeDto> PopulateUnitTypes()
        {
            var unitTypes = new List<UnitTypeDto>();
            for (int i = 1; i < 8; i++)
            {
                unitTypes.Add(
                    new UnitTypeDto()
                    {
                        Id = (short)i,
                        PortfolioId = 2,
                        Type = $"Unit Type {i}",
                        Active = i != 2 && i != 4,
                        CreateDate = DateTime.Now.AddMonths(-1),
                        CreateUserId = 1,
                        AmendDate = DateTime.Now.AddMonths(-1),
                        AmendUserId = 1,
                    }
                );
            }
            return unitTypes;
        }

    }
}
