namespace PropertyPortfolioManager.Models.Dto.Finance
{
    public class TransactionDetailDto
    {
        public int Id { get; set; }

        public int TransactionId { get; set; }

        public string Type { get; set; } = string.Empty;

        public int AccountId { get; set; }

        public string Account { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Reference { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Credit { get; set; }

        public decimal Debit { get; set; }
    }
}
