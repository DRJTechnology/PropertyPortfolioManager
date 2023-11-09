using PropertyPortfolioManager.Models.Dto.General;
using System.Data.SqlTypes;

namespace PropertyPortfolioManager.Models.Dto.Property
{
    public class UnitDto : BaseDto
    {
        public UnitDto()
        {
            this.Address = new AddressDto();
        }
        public int PortfolioId { get; set; }

        public string Code { get; set; } = string.Empty;

        public int AddressId { get; set; }

        public int UnitTypeId { get; set; }

        public String UnitType { get; set; } = string.Empty;

        public SqlMoney? PurchasePrice { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public SqlMoney? SalePrice { get; set; }

        public DateTime? SaleDate { get; set; }

        public AddressDto Address { get; set; }

        public bool Active { get; set; }
        public string MainPictureId { get; set; } = string.Empty;
    }
}
