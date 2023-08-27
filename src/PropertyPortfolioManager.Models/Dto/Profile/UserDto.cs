using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyPortfolioManager.Models.Dto.Profile
{
    public class UserDto
    {
        public int Id { get; set; }
        public Guid ObjectIdentifier { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
