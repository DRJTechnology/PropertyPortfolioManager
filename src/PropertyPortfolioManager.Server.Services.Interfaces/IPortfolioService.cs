﻿using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using System.Security.Claims;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<List<PortfolioModel>> GetAll(int portfolioId, bool activeOnly);
        Task<PortfolioModel> GetById(int portfolioId, int userId);
        Task<int> Create(int currentUserId, PortfolioModel portfolio);
        Task<bool> Update(int currentUserId, PortfolioModel portfolio);
        Task<PortfolioModel> GetCurrent(ClaimsPrincipal user);
        Task<bool> SelectForUser(int portfolioId, int userId, ClaimsPrincipal user);
        Task<bool> Delete(int currentUserId, int portfolioId);
    }
}
