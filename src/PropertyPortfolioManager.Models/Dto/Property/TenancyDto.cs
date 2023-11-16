namespace PropertyPortfolioManager.Models.Dto.Property
{
    public class TenancyDto : BaseDto
    {
        public int TenancyTypeId { get; set; }

        public String Type { get; set; } = string.Empty;

        public int UnitId { get; set; }

        public String StreetAddress { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Int16 DurationQuantity { get; set; }

        public Int16 DurationUnitId { get; set; }

        public bool ExpireAfterEndDate { get; set; }

        public bool Active { get; set; }
    }
}
