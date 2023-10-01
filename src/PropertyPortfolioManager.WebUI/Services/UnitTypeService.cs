﻿using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Helpers;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.Services
{
    public class UnitTypeService : IUnitTypeService
    {
        private readonly ILogger<UnitService> logger;
        private readonly IPpmApiFacade ppmApiFacade;

        public UnitTypeService(ILogger<UnitService> logger, IPpmApiFacade ppmApiFacade)
        {
            this.logger = logger;
            this.ppmApiFacade = ppmApiFacade;
        }

        public async Task<int> Create(UnitTypeModel unit)
        {
            try
            {
                return await this.ppmApiFacade.PostAsync<UnitTypeModel>(unit, "UnitType/Create");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UnitTypeModel>> GetAll()
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<List<UnitTypeModel>>("UnitType/GetAll");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UnitTypeModel> GetById(int unitTypeId)
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<UnitTypeModel>($"UnitType/GetById/{unitTypeId}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update( UnitTypeModel unit)
        {
            try
            {
                var unitTypeId = await this.ppmApiFacade.PostAsync<UnitTypeModel>(unit, "UnitType/Update");
                return unitTypeId > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
