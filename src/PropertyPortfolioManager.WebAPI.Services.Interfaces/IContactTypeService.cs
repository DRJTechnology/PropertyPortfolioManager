﻿using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.WebAPI.Services.Interfaces
{
    public interface IContactTypeService
    {
        Task<List<ContactTypeModel>> GetAll(int portfolioId);
        Task<ContactTypeModel> GetById(int contactId, int portfolioId);
        Task<int> Create(int currentUserId, int portfolioId, ContactTypeModel contact);
        Task<bool> Update(int currentUserId, int portfolioId, ContactTypeModel contact);
    }
}
