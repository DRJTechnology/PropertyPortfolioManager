using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.State
{
    public class ProfileState
    {
        public event Action OnChange;

        private PortfolioModel currentPortfolio;

        public PortfolioModel CurrentPortfolio
        {
            get
            {
                return currentPortfolio;
            }
            set
            {
                currentPortfolio = value;
                OnChange?.Invoke();
            }
        }
    }
}