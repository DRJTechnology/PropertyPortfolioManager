using PropertyPortfolioManager.Models.Dto.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyPortfolioManager.Models.Dto.Property
{
    public class UnitDto : BaseDto
    {
        public UnitDto()
        {
            this.Address = new AddressDto();
        }
        public string Code { get; set; } = string.Empty;
        public int AddressId { get; set; }

        public AddressDto Address { get; set; }
    }
}
