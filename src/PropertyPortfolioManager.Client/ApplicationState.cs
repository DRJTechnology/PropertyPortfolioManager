using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client
{
    public class ApplicationState
    {
        private readonly string currentPortfolioName;

        public String CurrentPortfolioName {
            get
            {
                return currentPortfolioName;
            }
            set
            {
                currentPortfolioName = value;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

https://stackoverflow.com/questions/73561406/listening-to-state-changes-when-a-property-value-is-changed-in-a-service