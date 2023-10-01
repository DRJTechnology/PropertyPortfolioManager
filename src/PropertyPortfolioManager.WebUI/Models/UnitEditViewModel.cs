using Microsoft.AspNetCore.Mvc.Rendering;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebUI.Models
{
    public class UnitEditViewModel
    {
        public UnitEditViewModel()
        {
            UnitTypeList = null;
            Unit = new UnitResponseModel();
        }

        //public List<UnitTypeModel> UnitTypeList { get; set; }
        public SelectList UnitTypeList { get; set; }
        public UnitResponseModel Unit { get; set; }
    }
}
