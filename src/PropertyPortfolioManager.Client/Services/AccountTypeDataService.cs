﻿using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.General;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Services
{
    public class AccountTypeDataService : IAccountTypeDataService
    {
        protected HttpClient httpClient { get; }

        public AccountTypeDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<EntityTypeBasicModel>> GetAllAsync()
        {
            try
            {
                var returnVal = await httpClient.GetFromJsonAsync<IEnumerable<EntityTypeBasicModel>>($"api/AccountType/GetAll");
                return returnVal;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
