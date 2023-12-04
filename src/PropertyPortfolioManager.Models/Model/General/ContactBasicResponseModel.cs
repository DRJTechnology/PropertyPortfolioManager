using System.ComponentModel;

namespace PropertyPortfolioManager.Models.Model.General
{
    public class ContactBasicResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [DisplayName("Street Address")]
        public string StreetAddress { get; set; } = string.Empty;

        public bool Active { get; set; }
    }
}
