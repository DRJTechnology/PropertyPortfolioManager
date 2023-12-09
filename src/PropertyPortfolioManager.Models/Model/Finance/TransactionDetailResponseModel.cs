using System.Data.SqlTypes;

namespace PropertyPortfolioManager.Models.Model.Finance
{
    public class TransactionDetailResponseModel : TransactionDetailEditModel
    {
        public string Account { get; set; }

        public string Type { get; set; }

    }
}
