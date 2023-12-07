namespace PropertyPortfolioManager.Models.Model.Finance
{
    public class TransactionEditModel
    {
        public int Id { get; set; }

        public int TransactionTypeId { get; set; }

        public DateTime Date { get; set; }

        public string Reference { get; set; } = string.Empty;

        public List<TransactionDetailEditModel> Details { get; set; }
    }
}
