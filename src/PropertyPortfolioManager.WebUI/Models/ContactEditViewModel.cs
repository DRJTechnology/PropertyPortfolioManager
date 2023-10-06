using Microsoft.AspNetCore.Mvc.Rendering;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebUI.Models
{
    public class ContactEditViewModel
    {
        public ContactEditViewModel()
        {
            ContactTypeList = null;
            Contact = new ContactResponseModel();
        }

        public SelectList ContactTypeList { get; set; }
        public ContactResponseModel Contact { get; set; }
    }
}
