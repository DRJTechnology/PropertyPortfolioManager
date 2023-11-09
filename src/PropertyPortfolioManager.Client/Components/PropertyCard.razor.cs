using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Components
{
    public partial class PropertyCard
    {
        [Parameter]
        public UnitBasicResponseModel Property { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        protected override void OnInitialized()
        {

        }
    }
}
