using PropertyPortfolioManager.Models.Model.General;
using System.ComponentModel;

namespace PropertyPortfolioManager.Models.Model.Property
{
    public class UnitBasicResponseModel
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        [DisplayName("Unit Type")]
        public string UnitType { get; set; } = string.Empty;

        [DisplayName("Street Address")]
        public string StreetAddress { get; set; } = string.Empty;

        public bool Active { get; set; }
    }
}
