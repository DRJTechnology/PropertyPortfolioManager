﻿using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IAccountTypeDataService
    {
        Task<IEnumerable<EntityTypeBasicModel>> GetAllAsync();
    }
}
