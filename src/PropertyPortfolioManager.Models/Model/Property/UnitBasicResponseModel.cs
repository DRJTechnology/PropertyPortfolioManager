using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Models.Model.Property
{
    public class UnitBasicResponseModel
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string UnitType { get; set; } = string.Empty;

        public string StreetAddress { get; set; } = string.Empty;
    }
}
