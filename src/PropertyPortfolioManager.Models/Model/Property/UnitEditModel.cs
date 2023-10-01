using PropertyPortfolioManager.Models.Model.General;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace PropertyPortfolioManager.Models.Model.Property
{
    public class UnitEditModel
    {
        public UnitEditModel()
        {
            this.Address = new AddressEditModel();
        }

        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public int AddressId { get; set; }

        [DisplayName("Unit Type")]
        public Int16  UnitTypeId { get; set; }

        [DisplayName("Purchase Date")]
        [DataType(DataType.Date)] 
        public DateTime? PurchaseDate { get; set; }

        [DisplayName("Purchase Price")]
        public SqlMoney? PurchasePrice { get; set; }

        [DisplayName("Sale Date")]
        [DataType(DataType.Date)]
        public DateTime? SaleDate { get; set; }

        [DisplayName("Sale Price")]
        public SqlMoney? SalePrice { get; set; }

        public bool Deleted { get; set; }

        public AddressEditModel Address { get; set; }
    }
}
