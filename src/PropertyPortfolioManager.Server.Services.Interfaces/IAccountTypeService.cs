﻿using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IAccountTypeService
    {
        Task<List<EntityTypeBasicModel>> GetAll();
    }
}
