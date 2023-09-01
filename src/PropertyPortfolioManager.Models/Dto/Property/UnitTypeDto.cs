namespace PropertyPortfolioManager.Models.Dto.Property
{
    public class UnitTypeDto : BaseDto
    {
        public new Int16 Id { get; set; }

        public string Type { get; set; } = string.Empty;
    }
}
