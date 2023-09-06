namespace PropertyPortfolioManager.Models.Model.General
{
    public class AddressEditModel
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; } = string.Empty;
        public string TownCity { get; set; } = string.Empty;
        public string CountyRegion { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
        public bool Deleted { get; set; }
    }
}
