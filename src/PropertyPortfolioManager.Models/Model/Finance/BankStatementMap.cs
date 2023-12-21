using CsvHelper.Configuration;

namespace PropertyPortfolioManager.Models.Model.Finance
{
    public class BankStatementMap : ClassMap<BankStatementModel>
    {
        public BankStatementMap()
        {
            Map(m => m.Date).Name("Date");
            Map(m => m.Amount).Name("Amount");
            Map(m => m.Description).Name("Memo");
            Map(m => m.TransactionType).Name("Subcategory");
        }
    }
}