using System.ComponentModel;

namespace PropertyPortfolioManager.Models.Model.General
{
    public class AddressModel
    {
        public int Id { get; set; }

        [DisplayName("Street Address")]
        public string StreetAddress { get; set; } = string.Empty;

        [DisplayName("Town/City")]
        public string TownCity { get; set; } = string.Empty;

        [DisplayName("County/Region")]
        public string CountyRegion { get; set; } = string.Empty;

        [DisplayName("Post Code")]
        public string PostCode { get; set; } = string.Empty;

        public bool Deleted { get; set; }
    }
}
