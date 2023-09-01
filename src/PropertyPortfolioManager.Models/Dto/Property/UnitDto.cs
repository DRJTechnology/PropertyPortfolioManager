using PropertyPortfolioManager.Models.Dto.General;

namespace PropertyPortfolioManager.Models.Dto.Property
{
    public class UnitDto : BaseDto
    {
        public UnitDto()
        {
            this.Address = new AddressDto();
            this.UnitType = new UnitTypeDto();
        }
        public string Code { get; set; } = string.Empty;

        public int AddressId { get; set; }

        public Int16 UnitTypeId { get; set; }

        public AddressDto Address { get; set; }

        public UnitTypeDto UnitType { get; set; }
    }
}
