using PropertyPortfolioManager.Models.Dto.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyPortfolioManager.Models.Dto.Property
{
    public class UnitBasicDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string UnitType { get; set; } = string.Empty;

        public string StreetAddress { get; set; } = string.Empty;
    }
}
