namespace PropertyPortfolioManager.Models.Model.Finance
{
    public class BankStatementModel
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
    }
}
