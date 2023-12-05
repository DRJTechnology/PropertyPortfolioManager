namespace PropertyPortfolioManager.Models.Model.Finance
{
    public class AccountEditModel
    {
        public int Id { get; set; }

        public int AccountTypeId { get; set; }

        public int PortfolioId { get; set; }

        public string AccountName { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public bool Active { get; set; }
    }
}
