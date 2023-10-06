namespace PropertyPortfolioManager.Models.Model.General
{
    public class ContactResponseModel : ContactEditModel
    {
        public ContactResponseModel()
        {
            this.Address = new AddressResponseModel();
        }
        public string ContactType { get; set; } = string.Empty;

        public new AddressResponseModel Address { get; set; }
    }
}
