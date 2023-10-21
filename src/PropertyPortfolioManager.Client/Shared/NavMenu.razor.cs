using Microsoft.AspNetCore.Components;

namespace PropertyPortfolioManager.Client.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}
