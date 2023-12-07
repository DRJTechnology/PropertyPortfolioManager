using System.Data.SqlTypes;

namespace PropertyPortfolioManager.Models.Model.Finance
{
    public class TransactionDetailEditModel
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public DateTime Date { get; set; }

        public string Details { get; set; } = string.Empty;

        public SqlMoney Amount { get; set; }

        public Int16 Direction { get; set; }
    }
}
