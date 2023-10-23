namespace PropertyPortfolioManager.Models.Model.General
{
    public class ContactResponseModel : ContactEditModel
    {
        public ContactResponseModel()
        {
            this.Address = new AddressModel();
        }
        public string ContactType { get; set; } = string.Empty;

        public new AddressModel Address { get; set; }
    }
}
