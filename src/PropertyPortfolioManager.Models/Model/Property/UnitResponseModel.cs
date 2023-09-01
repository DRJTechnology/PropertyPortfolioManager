using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Models.Model.Property
{
    public class UnitResponseModel : UnitEditModel
    {
        public UnitResponseModel()
        {
            this.Address = new AddressResponseModel();
        }

        public new AddressResponseModel Address { get; set; }
    }
}
