namespace PropertyPortfolioManager.Models.Model.Property
{
    public class TenancyResponseModel : TenancyEditModel
    {
        public string TenancyType { get; set; } = string.Empty;
        public string StreetAddress { get; set; } = string.Empty;
    }
}
