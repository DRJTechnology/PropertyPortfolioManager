using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Dto.Profile;

namespace PropertyPortfolioManager.Client.Pages
{
    public partial class UserProfile
    {
        private UserDto User = new UserDto();

        [Inject]
        public IUserDataService userDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                User = await this.userDataService.GetCurrentAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
