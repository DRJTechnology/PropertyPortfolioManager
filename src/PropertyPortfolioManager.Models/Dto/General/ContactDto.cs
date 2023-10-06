using System.Data.SqlTypes;

namespace PropertyPortfolioManager.Models.Dto.General
{
    public class ContactDto : BaseDto
    {
        public ContactDto()
        {
            this.Address = new AddressDto();
        }

        public int PortfolioId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int AddressId { get; set; }

        public int ContactTypeId { get; set; }

        public String ContactType { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public bool Active { get; set; }

        public AddressDto Address { get; set; }
    }
}
