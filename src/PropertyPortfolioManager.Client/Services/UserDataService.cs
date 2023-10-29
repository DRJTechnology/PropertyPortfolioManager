using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Dto.Profile;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Services
{
    public class UserDataService : GenericDataService,  IUserDataService
    {
        public UserDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "User";
        }

        public async Task<UserDto> GetCurrentAsync()
        {
            return await httpClient.GetFromJsonAsync<UserDto>($"api/User/GetCurrent");
        }
    }
}
