using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Models.Model.Property;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Services
{
    public class TenancyDataService : GenericDataService, ITenancyDataService
    {
        public TenancyDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "Tenancy";
        }

        public async Task<ContactResponseModel> AddContact(TenancyContactModel tenancyContact)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<TenancyContactModel>($"api/Tenancy/AddContact", tenancyContact);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to add tenancy contact.");
                }

                var returnValue = await response.Content.ReadFromJsonAsync<ContactResponseModel>();

                return returnValue;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RemoveContact(TenancyContactModel tenancyContact)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<TenancyContactModel>($"api/Tenancy/RemoveContact", tenancyContact);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to remove tenenacy contact.");
                }

                var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

                return returnValue.Success;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
