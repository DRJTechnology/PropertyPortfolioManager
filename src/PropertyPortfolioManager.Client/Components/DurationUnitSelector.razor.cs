using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Models.Enums;

namespace PropertyPortfolioManager.Client.Components
{
    public partial class DurationUnitSelector
    {
        private DurationUnitEnum[]? durationunits;
        private bool Initialising = true;
        private DurationUnitEnum selectedDurationUnit;

        [Parameter]
        public string ElementId { get; set; } = "durationunit";

        [Parameter]
        public string ElementClass { get; set; } = string.Empty;

        [Parameter]
        public DurationUnitEnum DurationUnit { get; set; } = 0;

        [Parameter]
        public EventCallback<DurationUnitEnum> DurationUnitChanged { get; set; }

        protected override void OnInitialized()
        {
            selectedDurationUnit = DurationUnit;
            PopulateDurationUnitListAsync();
            Initialising = false;
        }

        private void PopulateDurationUnitListAsync()
        {
            try
            {
                durationunits = (DurationUnitEnum[])Enum.GetValues(typeof(DurationUnitEnum));
                Initialising = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private DurationUnitEnum SelectedDurationUnitId
        {
            get
            {
                return selectedDurationUnit;
            }
            set
            {
                if (selectedDurationUnit != value)
                {
                    selectedDurationUnit = value;
                    DurationUnitChanged.InvokeAsync(selectedDurationUnit);
                }
            }
        }
    }
}
