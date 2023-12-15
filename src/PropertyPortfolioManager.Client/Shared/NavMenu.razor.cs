using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using PropertyPortfolioManager.Client.State;

namespace PropertyPortfolioManager.Client.Shared
{
    public partial class NavMenu
    {
        [Inject]
        public ProfileState ProfileState { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        protected override void OnInitialized()
        {
            ProfileState.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            ProfileState.OnChange -= StateHasChanged;
        }
        public void BeginLogOut()
        {
            Navigation.NavigateToLogout("authentication/logout");
        }

    }
}
