using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyPortfolioManager.Models.Dto.General
{
    public class AddressDto : BaseDto
    {
        public string StreetAddress { get; set; } = string.Empty;
        public string TownCity { get; set; } = string.Empty;
        public string CountyRegion { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
    }
}
