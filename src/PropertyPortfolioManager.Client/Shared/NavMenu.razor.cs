using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.State;

namespace PropertyPortfolioManager.Client.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;

        [Inject]
        public ProfileState ProfileState { get; set; }

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected override void OnInitialized()
        {
            ProfileState.OnChange += StateHasChanged;
        }
        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        public void Dispose()
        {
            ProfileState.OnChange -= StateHasChanged;
        }
    }
}
