using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Enums;
using PropertyPortfolioManager.Models.Model.General;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PropertyPortfolioManager.Models.Model.Property
{
    public class TenancyEditModel
    {
        public int Id { get; set; }

        [DisplayName("Tenancy Type")]
        public int TenancyTypeId { get; set; }

        [DisplayName("Property")]
        public int UnitId { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [DisplayName("Duration")]
        public Int16 DurationQuantity { get; set; }
        public DurationUnitEnum DurationUnit { get; set; }

        public bool ExpiresAfterEndDate { get; set; }

        public bool Active { get; set; }

        public List<ContactResponseModel> Contacts { get; set; }

        public bool Deleted { get; set; }
    }
}
