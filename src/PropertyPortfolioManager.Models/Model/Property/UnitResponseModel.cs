using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Models.Model.Property
{
    public class UnitResponseModel : UnitEditModel
    {
        public UnitResponseModel()
        {
            this.Address = new AddressResponseModel();
            this.UnitType = new UnitTypeModel();
        }

        public new AddressResponseModel Address { get; set; }
        public new UnitTypeModel UnitType { get; set; }
    }
}
