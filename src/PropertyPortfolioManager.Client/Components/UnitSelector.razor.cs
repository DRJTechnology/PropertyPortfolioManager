using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Components
{
    public partial class UnitSelector
    {
        private IEnumerable<UnitBasicResponseModel>? units;
        private bool Initialising = true;
        private int selectedUnitId = 0;

        [Inject]
        public IUnitDataService unitDataService { get; set; }

        [Parameter]
        public bool ActiveOnly { get; set; } = true;

        [Parameter]
        public string ElementId { get; set; } = "unit";

        [Parameter]
        public string ElementClass { get; set; } = string.Empty;

        [Parameter]
        public int UnitId { get; set; } = 0;

        [Parameter]
        public EventCallback<int> UnitIdChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            selectedUnitId = UnitId;
            await PopulateUnitListAsync();
            Initialising = false;
        }

        private async Task PopulateUnitListAsync()
        {
            try
            {
                units = await this.unitDataService.GetAllAsync<UnitBasicResponseModel>(ActiveOnly);
                Initialising = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private int SelectedUnitId
        {
            get
            {
                return selectedUnitId;
            }
            set
            {
                if (selectedUnitId != value)
                {
                    selectedUnitId = value;
                    UnitIdChanged.InvokeAsync(selectedUnitId);
                }
            }
        }
    }
}
