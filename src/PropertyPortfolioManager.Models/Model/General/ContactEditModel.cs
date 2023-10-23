using System.ComponentModel;

namespace PropertyPortfolioManager.Models.Model.General
{
    public class ContactEditModel
    {
        public ContactEditModel()
        {
            this.Address = new AddressModel();
        }

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int AddressId { get; set; }

        [DisplayName("Contact Type")]
        public int ContactTypeId { get; set; }

        public string Notes { get; set; } = string.Empty;

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public AddressModel Address { get; set; }
    }
}
