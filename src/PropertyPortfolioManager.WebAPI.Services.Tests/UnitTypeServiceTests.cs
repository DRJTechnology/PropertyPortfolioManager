using Moq;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyPortfolioManager.WebAPI.Services.Tests
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
            var unitTypeRepositoryMock = new Mock<IUnitTypeRepository>(MockBehavior.Strict);
            unitTypeRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(Task.FromResult(this.unitTypeList));

            var unitTypeService = new UnitTypeService(unitTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var units = await unitTypeService.GetAll();

            Assert.IsType<List<UnitTypeModel>>(units);
            Assert.Equal(7, units.Count());
            Assert.Equal(1, units.FirstOrDefault().Id);
        }

        [Fact]
        public async void Get_UnitType_By_Id()
        {
            var unitTypeId = 4;
            var unitTypeRepositoryMock = new Mock<IUnitTypeRepository>(MockBehavior.Strict);
            unitTypeRepositoryMock.Setup(r => r.GetById(unitTypeId))
                                        .Returns(Task.FromResult(this.GetById(unitTypeId)));

            var unitTypeService = new UnitTypeService(unitTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unit = await unitTypeService.GetById(unitTypeId);

            Assert.IsType<UnitTypeModel>(unit);
            Assert.Equal(unitTypeId, unit.Id);
            Assert.Equal($"Unit Type {unitTypeId}", unit.Type);
        }


        [Fact]
        public async void Get_UnitType_By_Id_NotFound()
        {
            var unitTypeId = 33;
            var unitTypeRepositoryMock = new Mock<IUnitTypeRepository>(MockBehavior.Strict);
            unitTypeRepositoryMock.Setup(r => r.GetById(unitTypeId))
                                        .Returns(Task.FromResult(this.GetById(unitTypeId)));

            var unitTypeService = new UnitTypeService(unitTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unit = await unitTypeService.GetById(unitTypeId);

            Assert.Null(unit);
        }

        [Fact]
        public async void Create_UnitType()
        {
            var currentUserId = 2;
            var newUnit = new UnitTypeModel()
            {
                Type = "TestUnitType1",
            };

            var unitTypeRepositoryMock = new Mock<IUnitTypeRepository>(MockBehavior.Strict);
            unitTypeRepositoryMock.Setup(r => r.Create(It.IsAny<int>(), It.IsAny<UnitTypeDto>()))
                                        .Returns(Task.FromResult(11));

            var unitTypeService = new UnitTypeService(unitTypeRepositoryMock.Object, null, TestExtensions.MapperInstance());
            var unitTypeId = await unitTypeService.Create(currentUserId, newUnit);

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
                        Type = $"Unit Type {i}",
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
